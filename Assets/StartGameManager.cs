using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public TMP_InputField playerNameText;
    public string playerName;

    public static StartGameManager Instance;

    bool klaar;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !klaar)
        {
            playerName = playerNameText.text;
            SceneManager.LoadScene("LisaScene");
            klaar = true;
        }
    }
}
