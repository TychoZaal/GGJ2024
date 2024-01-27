using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Animator canvas;
    [SerializeField] private float duration = 1.0f;

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

    public void SpawnText(string text, Vector3 coordinates, float maxOffset, float minOffset)
    {
        float offset = Random.Range(minOffset, maxOffset);
        bool negative = Random.Range(0.0f, 1.0f) > 0.5f;
        offset = negative ? offset * -1.5f : offset;

        var textObject = Instantiate(canvas, coordinates +
            new Vector3(coordinates.x + offset,
                negative ? coordinates.y + -offset : coordinates.y + offset, 0), Quaternion.identity, transform);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
        textObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-25, 25));

        textObject.GetComponent<Animator>().SetTrigger("Scale");
        textObject.GetComponent<Animator>().SetFloat("Speed", 1 / duration);

        Destroy(textObject.gameObject, duration);
    }
}
