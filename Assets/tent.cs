using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("guy"))
        {
            GetComponent<Animator>().SetTrigger("tent");


            HighscoreManager._instance.StartCoroutine(HighscoreManager._instance.AssessCreatedCharacter());




            Debug.Log("sloeppie");

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
