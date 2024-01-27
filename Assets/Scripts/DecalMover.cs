using UnityEngine;

public class DecalMover : MonoBehaviour
{
    public bool isStopped = false;

    [SerializeField]
    Transform startLocation;
    [SerializeField]
    Transform endLocation;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (isStopped) return;

        if (this.transform.position == this.endLocation.position)
        {
            this.transform.position = this.startLocation.position;
            this.startTime = Time.time;
        }

        this.transform.position = Vector3.MoveTowards(this.startLocation.position, this.endLocation.position, getCurrentTimeInPosition());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.isStopped = true;
        }
    }

    float getCurrentTimeInPosition()
    {
        return 0f + (Time.time - this.startTime);
    }
}
