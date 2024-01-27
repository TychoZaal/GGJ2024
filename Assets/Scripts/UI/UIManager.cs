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

    public void SpawnText(string text, Vector3 coordinates, float offset)
    {
        var textObject = Instantiate(canvas, coordinates +
            new Vector3(Random.Range(coordinates.x - offset, coordinates.x + offset),
                Random.Range(coordinates.y, coordinates.y + offset), 0), Quaternion.identity, transform);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
        textObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-25, 25));

        textObject.GetComponent<Animator>().SetTrigger("Scale");
        textObject.GetComponent<Animator>().SetFloat("Speed", 1 / duration);

        Destroy(textObject.gameObject, duration);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnText("+" + Random.Range(0, 1000), new Vector3(0, 0, -0.75f), 0.5f);
        }
    }
}
