using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI name, score;

    private void Start()
    {
        name.text = StartGameManager.Instance.playerName;
        score.text = HighscoreManager._instance.GetHighscore().ToString() + " laugs!";
    }
}
