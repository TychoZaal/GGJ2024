using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    // This script moves the object left and right along the x axis
    [SerializeField]
    float distance = 10.0f;
    [SerializeField]
    float speed = 2.0f;

    void Update()
    {
        // Move the object left and right along the x axis
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, distance), transform.position.y, transform.position.z);
    }
}
