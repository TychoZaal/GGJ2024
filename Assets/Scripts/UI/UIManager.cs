using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Animator canvas;
    [SerializeField] private float duration = 1.0f;
    [SerializeField] public List<Color> hahaColors = new List<Color>();
    [SerializeField] public List<Color> pointColors = new List<Color>();
    [SerializeField] public List<Transform> playerHeadTextLocations = new List<Transform>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnText(string text, Transform transform, List<Color> colorPallette)
    {
        var textObject = Instantiate(canvas, Vector3.zero, Quaternion.identity, transform);
        textObject.transform.position = transform.position;
        textObject.transform.localEulerAngles = new Vector3(0, Random.Range(-25, 25), 0);

        var textMesh = textObject.GetComponentInChildren<TextMeshProUGUI>();
        string textColored = $"";

        foreach (char c in text)
        {
            Color color = colorPallette[Random.Range(0, colorPallette.Count)];
            textColored += $"{c.ToString().AddColor(color)}";
        }

        textMesh.SetText(textColored);

        textObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-25, 25));

        float time = 0, goal = Random.Range(0, 0.7f);

        while (time < goal)
        {
            time += Time.deltaTime;
        }

        textObject.GetComponent<Animator>().SetTrigger("Scale");
        textObject.GetComponent<Animator>().SetFloat("Speed", 1 / (duration + Random.Range(0, 0.75f)));

        Destroy(textObject.gameObject, duration);
    }
}

public static class StringExtensions
{
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
}
