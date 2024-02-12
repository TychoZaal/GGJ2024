using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public int currentIndex = 0;
    DecalMover currentDecalMover = null;
    private List<GameObject> gameOrder;

    public GameObject hangingGuyPrefab = null;
    public HaningGuy hangingGuyScript = null;
    private GameObject currentGuy = null;
    private GameObject hook = null;

    [SerializeField] public int secondsToPlay = 60, secondsPassed = 0;
    [SerializeField] public TextMeshProUGUI timeLeftText;

    [SerializeField] private MoveHookAlongRail mhar;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private AudioClip belt;

    public int currentMaximumFaceProperties = 0;
    public float gameSpeed = 1f;
    public float slowdownLength = 2.0f;
    public float slowdownFactor = 0.075f;
    // Start is called before the first frame update

    public float scaleUpAmount = 2.0f;
    public float scaleDownAmount = 0.5f;
    public float scaleSpeed = 1.0f;

    private Vector3 originalScale;
    private bool isScalingUp = false;
    private bool shouldScale = false;

    public AudioClip tick, whistle;
    public AudioSource bgm;

    [HideInInspector] public bool canPressSpace = false;


    bool isDone = false, isTutorial = true;
    void Start()
    {

        //if (gameOrder.Count <= 0)
        //{
        //    return;
        //}

        //currentMaximumFaceProperties = gameOrder.Count;

        //InitializeNewGuy();
        timeLeftText.text = (secondsToPlay - secondsPassed).ToString();
        StartCoroutine(Tutorial());
        originalScale = timeLeftText.transform.localScale;
    }

    private IEnumerator Tutorial()
    {
        canPressSpace = false;

        GameObject currentGuy = Instantiate(hangingGuyPrefab, new Vector3(spawnPos.position.x, 0.0f, 0.0f), new Quaternion());
        hangingGuyScript = currentGuy.GetComponent<HaningGuy>();
        hook = hangingGuyScript.hook;
        mhar.TriggerSpawnAnimation(hook);

        foreach (var decal in hangingGuyScript.DecalProjectors)
        {
            decal.SetActive(true);
            decal.GetComponent<DecalMover>().isStopped = true;
            decal.transform.localEulerAngles = new Vector3(decal.transform.localEulerAngles.x, decal.transform.localEulerAngles.y, 0);
            decal.transform.parent.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(3.0f);

        foreach (var decal in hangingGuyScript.DecalProjectors)
        {
            decal.GetComponent<DecalMover>().MockShowScore();
        }

        HighscoreManager._instance.CalculateCreatedCharacterScore();

        yield return new WaitForSeconds(1.0f);

        mhar.TriggerMoveAway(hook);

        yield return new WaitForSeconds(6.0f);

        StartCoroutine(GameTimer());
        InitializeNewGuy();
        SetCurrentActiveObject();

        canPressSpace = true;
        isTutorial = false;
    }

    private IEnumerator GameTimer ()
    {
        timeLeftText.text = secondsToPlay.ToString();

        while (secondsPassed < secondsToPlay)
        {
            yield return new WaitForSeconds(1);
            secondsPassed += 1;
            StartScalingUp();
            SoundManager._instance.PlaySound(transform, tick, 0.85f, 0.1f);

            if (secondsPassed / secondsToPlay > 0.60f)
            {
                bgm.pitch = 1.5f;
            }
            if (secondsPassed / secondsToPlay > 0.80f)
            {
                bgm.pitch = 1.8f;
            }

            timeLeftText.text = (secondsToPlay - secondsPassed).ToString();
        }

        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        timeLeftText.text = "0";
        HighscoreManager._instance.StoreHighestScore();
        SoundManager._instance.PlaySound(transform, whistle, 0.7f);

        float t = 0;
        while (t < slowdownLength)
        {
            Time.timeScale = Mathf.Lerp(1f, slowdownFactor, t / slowdownLength);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = slowdownFactor;

        yield return new WaitForSecondsRealtime(1.0f);

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameEnd");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial) return;

        if (currentDecalMover.isStopped)
        {
            currentIndex++;
            if (currentIndex >= currentMaximumFaceProperties)
            {
                if (!isDone)
                {
                    canPressSpace = false;
                    isDone = true;
                    HighscoreManager._instance.CalculateCreatedCharacterScore();
                    SoundManager._instance.PlaySound(transform, belt, 0.5f);
                    StartCoroutine(WaitBeforeSpawningNextGuy());
                }
            }
            else
            {
                SetCurrentActiveObject();
            }
        }

        if (shouldScale)
        {
            if (isScalingUp)
            {
                timeLeftText.transform.localScale = Vector3.Lerp(timeLeftText.transform.localScale, originalScale * scaleUpAmount, Time.deltaTime * scaleSpeed);
                if (Vector3.Distance(timeLeftText.transform.localScale, originalScale * scaleUpAmount) < 0.01f)
                {
                    isScalingUp = false;
                }
            }
            else
            {
                timeLeftText.transform.localScale = Vector3.Lerp(timeLeftText.transform.localScale, originalScale * scaleDownAmount, Time.deltaTime * scaleSpeed);
                if (Vector3.Distance(timeLeftText.transform.localScale, originalScale * scaleDownAmount) < 0.01f)
                {
                    shouldScale = false;
                }
            }
        }
    }

    IEnumerator WaitBeforeSpawningNextGuy()
    {
        yield return new WaitForSeconds(1f);
        mhar.TriggerMoveAway(hook);
        InitializeNewGuy();
        SetCurrentActiveObject();
    }

    void InitializeNewGuy()
    {
        GameObject currentGuy = Instantiate(hangingGuyPrefab, new Vector3(spawnPos.position.x, 0.0f, 0.0f), new Quaternion());
        hangingGuyScript = currentGuy.GetComponent<HaningGuy>();
        hook = hangingGuyScript.hook;
        gameOrder = hangingGuyScript.DecalProjectors;
        currentMaximumFaceProperties = gameOrder.Count;
        isDone = false;
        currentIndex = 0;
        mhar.TriggerSpawnAnimation(hook);
    }

    void SetCurrentActiveObject()
    {
        currentDecalMover = gameOrder[currentIndex].GetComponent<DecalMover>();
        gameOrder[currentIndex].transform.parent.gameObject.SetActive(true);
    }

    public void StartScalingUp()
    {
        isScalingUp = true;
        shouldScale = true;
    }

    public void StartScalingDown()
    {
        isScalingUp = false;
    }
}
