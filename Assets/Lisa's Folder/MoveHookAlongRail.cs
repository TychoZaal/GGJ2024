using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHookAlongRail : MonoBehaviour
{
    //public static MoveHookAlongRail Instance { get; private set; }


    //private void Awake()
    //{
    //    if (Instance != null)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}

    [SerializeField] GameObject spawnPosition;
    [SerializeField] List<GameObject> movepoints;
    // Start is called before the first frame update
    void Start()
    {

        //LTSeq sequence = LeanTween.sequence();

        //sequence.append(LeanTween.move(gameObject, movepoints[0].transform.position, 2.0f).setEase(LeanTweenType.easeInOutQuad));
        //sequence.append(LeanTween.move(gameObject, movepoints[1].transform.position, 0.1f));
        //sequence.append(LeanTween.move(gameObject, movepoints[2].transform.position, 3.0f).setEase(LeanTweenType.easeInOutQuad));
        //sequence.append(LeanTween.move(gameObject, movepoints[4].transform.position, 0.1f));
        //sequence.append(LeanTween.move(gameObject, movepoints[3].transform.position, 3.0f).setEase(LeanTweenType.easeInOutQuad));
    }

    public void TriggerMoveAway(GameObject hook)
    {
        LTSeq sequence = LeanTween.sequence();
        //time passed since start of game
        float timePassed = Time.timeSinceLevelLoad;
        float timeToSpawn = 3.0f - (timePassed / 40f);
        sequence.append(LeanTween.move(hook, movepoints[0].transform.position, timeToSpawn).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.move(hook, movepoints[1].transform.position, 0.1f));
        sequence.append(LeanTween.move(hook, movepoints[2].transform.position, timeToSpawn).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.move(hook, movepoints[4].transform.position, 0.1f));
        sequence.append(LeanTween.move(hook, movepoints[3].transform.position, timeToSpawn).setEase(LeanTweenType.easeInOutQuad));
    }

    public void TriggerSpawnAnimation(GameObject hook)
    {
        //time passed since start of game
        float timePassed = Time.timeSinceLevelLoad;
        //decrease the time to spawn the hook by 0.1 seconds for every 10 seconds passed
        float timeToSpawn = 2.0f - (timePassed / 40f);
        LeanTween.move(hook, spawnPosition.transform.position, timeToSpawn).setEase(LeanTweenType.easeInOutQuad);
    }

}
