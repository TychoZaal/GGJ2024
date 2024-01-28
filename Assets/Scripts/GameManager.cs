using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private MoveHookAlongRail mhar;
    [SerializeField] private Transform spawnPos;

    public int currentMaximumFaceProperties = 0;
    public float gameSpeed = 1f;
    // Start is called before the first frame update

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
                    isDone = true;
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
        //HighscoreManager._instance.Invoke("AssessCreatedCharacter", 2.0f);
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
