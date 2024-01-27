using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public static ScoreCalculator Instance { get; private set; }

    [SerializeField] private float maxDistance;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, maxDistance);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else Instance = this;
    }

    public float perfectScore = 1000;

    public int CalculateScore(Vector3 expectedPosition, Vector3 actualPosition)
    {
        float distance = Vector3.Distance(expectedPosition, actualPosition);

        return Mathf.RoundToInt(Mathf.Clamp(perfectScore - distance / maxDistance * perfectScore, 0, perfectScore));
    }
}
