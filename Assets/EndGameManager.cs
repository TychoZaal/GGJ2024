using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI name, score;

    private void Start()
    {
        name.text = StartGameManager.Instance.playerName;
        score.text = HighscoreManager._instance.GetHighscore().ToString() + " laughs!";
        Invoke("ShowHighscores", 3.0f);
    }

    void ShowHighscores()
    {
        SceneManager.LoadScene("HighscoreOverview");
    }
}
