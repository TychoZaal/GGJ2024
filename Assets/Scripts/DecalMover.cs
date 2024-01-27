using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalMover : MonoBehaviour
{
    public bool isStopped = false;

    [SerializeField]
    Transform startLocation;
    [SerializeField]
    Transform endLocation;

    public GameObject planeGO;

    public Asset.Category category;

    float startTime;

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
            }

            this.isStopped = true;
            planeGO.SetActive(false);
        }
    }

    float getCurrentTimeInPosition()
    {
        return 0f + (Time.time - this.startTime);
    }
}
