using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class DecalMover : MonoBehaviour
{
    public bool isStopped = false;

    [SerializeField]
    Transform startLocation;
    [SerializeField]
    Transform endLocation;

    public GameObject planeGO;

    [SerializeField] private List<Transform> expectedLandingPositions = new List<Transform>();

    public Asset.Category category;
    float startTime;

    private Vector3 hitPoint = Vector3.positiveInfinity;
    private Vector3 expectedLanding = Vector3.positiveInfinity;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(hitPoint, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(expectedLanding, 0.1f);
    }

    void Start()
    {
        Texture2D texture = AssetManager.Instance.GetRandomAssetOfCategory(category) as Texture2D;

        DecalProjector projector = gameObject.GetComponent<DecalProjector>();
        Material newDecalMaterial = new Material(projector.material);
        newDecalMaterial.SetTexture("Base_Map", texture);

        projector.material = newDecalMaterial;
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
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name != "Head")
                {
                    Destroy(this.transform.parent.gameObject);
                }

                hitPoint = hit.point;
            }

            ShowScore(hit.point);

            this.isStopped = true;
            planeGO.SetActive(false);
        }
    }

    private Vector3 FindNearestLandingPosition(Vector3 hitPosition)
    {
        expectedLanding = expectedLandingPositions.OrderBy(landingPosition => Vector3.Distance(hitPosition, landingPosition.position)).FirstOrDefault().transform.position;
        return expectedLandingPositions.OrderBy(landingPosition => Vector3.Distance(hitPosition, landingPosition.position)).FirstOrDefault().transform.position;
    }
    
    public void ShowScore(Vector3 hitPosition)
    {
        int score = ScoreCalculator.Instance.CalculateScore(FindNearestLandingPosition(hitPosition), hitPosition);
        HighscoreManager._instance.AddScore(new PlayerRecord
        {
            playerName = HighscoreManager._instance.playerName,
            highScore = score
        });
    }

    float getCurrentTimeInPosition()
    {
        float result = 0f + (Time.time - this.startTime) * GameManager.Instance.gameSpeed;
        return result;
    }
}
