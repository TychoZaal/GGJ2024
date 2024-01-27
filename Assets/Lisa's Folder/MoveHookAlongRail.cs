using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHookAlongRail : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
