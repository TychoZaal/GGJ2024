using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startcheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("guy"))
        {
            GameManager.Instance.canPressSpace = true;
        }
    }
}
