using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    // Start is called before the first frame update

    [HideInInspector] public bool canPressSpace = false;


    bool isDone = false;
    void Start()
    {

        //if (gameOrder.Count <= 0)
        //{
        //    return;
        //}

        //currentMaximumFaceProperties = gameOrder.Count;

        InitializeNewGuy();
        SetCurrentActiveObject();
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer ()
    {
        timeLeftText.text = secondsToPlay.ToString();

        while (secondsPassed < secondsToPlay)
        {
            yield return new WaitForSeconds(1);
            secondsPassed += 1;

            timeLeftText.text = (secondsToPlay - secondsPassed).ToString();
        }

        EndGame();
    }

    private void EndGame()
    {
        timeLeftText.text = string.Empty;
        HighscoreManager._instance.StoreHighestScore();
    }

    // Update is called once per frame
    void Update()
    {
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
                    SoundManager._instance.PlaySound(transform, belt, 1.0f);
                    StartCoroutine(WaitBeforeSpawningNextGuy());
                }
            }
            else
            {
                SetCurrentActiveObject();
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
}
