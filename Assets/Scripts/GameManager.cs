using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    public int currentIndex = 0;
    DecalMover currentDecalMover = null;
    public List<GameObject> gameOrder = new List<GameObject>();
    public int currentMaximumFaceProperties = 0;
    public float gameSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (gameOrder.Count <= 0)
        {
            return;
        }

        currentMaximumFaceProperties = gameOrder.Count;
        SetCurrentActiveObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDecalMover.isStopped)
        {
            currentIndex++;
            if (currentIndex >= currentMaximumFaceProperties)
            {
                Debug.Log("done");
            }
            else
            {
                SetCurrentActiveObject();
            }
        }
    }

    void SetCurrentActiveObject()
    {
        currentDecalMover = gameOrder[currentIndex].GetComponent<DecalMover>();
        gameOrder[currentIndex].transform.parent.gameObject.SetActive(true);
    }
}
