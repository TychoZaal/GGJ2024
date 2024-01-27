using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Animator canvas;
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private List<Color> colors = new List<Color>();

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

    public void SpawnText(string text, Transform transform)
    {
        var textObject = Instantiate(canvas, Vector3.zero, Quaternion.identity, transform);

        var textMesh = textObject.GetComponentInChildren<TextMeshProUGUI>();
        string textColored = $"";

        foreach (char c in text)
        {
            Color color = colors[Random.Range(0, colors.Count)];
            textColored += $"{c.ToString().AddColor(color)}";
        }

        textMesh.SetText(textColored);

        textObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-25, 25));

        textObject.GetComponent<Animator>().SetTrigger("Scale");
        textObject.GetComponent<Animator>().SetFloat("Speed", 1 / duration);

        Destroy(textObject.gameObject, duration);
    }
}

public static class StringExtensions
{
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
}
