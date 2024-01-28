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

    [SerializeField] private List<Transform> expectedLandingPositions = new List<Transform>();

    public Asset.Category category;
    float startTime;

    public Vector3 rotation = Vector3.zero;
    private Vector3 hitPoint = Vector3.positiveInfinity;
    private Vector3 expectedLanding = Vector3.positiveInfinity;
    private float startY = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(hitPoint, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(expectedLanding, 0.1f);
        Gizmos.DrawRay(this.transform.position, this.transform.forward * 100f);
    }

    void Start()
    {
        rotation = new Vector3(0f, 0f, 0.2f);
        this.transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        Texture2D texture = AssetManager.Instance.GetRandomAssetOfCategory(category) as Texture2D;

        DecalProjector projector = gameObject.GetComponent<DecalProjector>();
        float min = -0.1f;
        float max = 0.1f;
        float randomSize = Random.Range(min, max);
        projector.transform.localScale += new Vector3(randomSize, randomSize, 0f);
        Material newDecalMaterial = new Material(projector.material);
        newDecalMaterial.SetTexture("Base_Map", texture);

        projector.material = newDecalMaterial;
        startTime = Time.time;
        startY = this.transform.parent.transform.parent.position.y;
        if (category == Asset.Category.MOUTH)
        {
            this.startLocation.position -= new Vector3(0.5f, 0f, 0f);
            this.endLocation.position += new Vector3(0.5f, 0f, 0f);
        }
    }
    private float SpeedScalarExponent => Mathf.Clamp(SpeedScalar * SpeedScalar, 1f, 10f);
    private float SpeedScalar => 1f + Time.timeSinceLevelLoad / 10f;
    private bool debugStop = false;
    void Update()
    {
        if (isStopped || debugStop) return;

        if (this.transform.position.x == this.endLocation.position.x)
        {
            Vector3 tempLocation = this.startLocation.position;
            this.startLocation.position = this.endLocation.position;
            this.endLocation.position = tempLocation;
            this.startTime = Time.time;
        }

        this.transform.Rotate(rotation * SpeedScalarExponent);

        Vector3 xStartLocation = new Vector3(this.startLocation.transform.position.x, this.transform.position.y, this.startLocation.transform.position.z);
        Vector3 xEndLocation = new Vector3(this.endLocation.transform.position.x, this.transform.position.y, this.endLocation.transform.position.z);

        this.transform.position = Vector3.MoveTowards(xStartLocation, xEndLocation, getCurrentTimeInPosition());

        this.transform.position = new Vector3(this.transform.position.x, startY + Mathf.Sin(Time.time) * 0.5f, this.transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.Instance.canPressSpace) return;
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, this.transform.forward * 100f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name != "Head" && hit.transform.gameObject.name != "Neck")
                {
                    Debug.Log("Hit " + hit.transform.gameObject.name);
                    return;
                }
                //hitPoint = hit.point;
                ShowScore(hit.point);

                this.isStopped = true;
            }
            else
            {
                debugStop = true;
                Debug.Log("Miss");
            }
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
        float result = 0f + (Time.time - this.startTime) * SpeedScalarExponent;
        return result;
    }
}
