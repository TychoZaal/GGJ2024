using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager _instance { get; private set; }

    public List<PlayerRecord> currentPlayerRecords = new List<PlayerRecord>();

    public HighscoreList highScoreList;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void AddScore(PlayerRecord playerRecord)
    {
        currentPlayerRecords.Add(playerRecord);
    }

    public void StoreHighestScore()
    {
        var sortedPlayerRecords = currentPlayerRecords.OrderByDescending(playerRecord => playerRecord.highScore);
        highScoreList.highScores.Add(sortedPlayerRecords.FirstOrDefault());
    }

    public List<PlayerRecord> LoadHighscores(int xAmount)
    {
        var sortedPlayerRecords = highScoreList.highScores.OrderByDescending(playerRecord => playerRecord.highScore);
        return sortedPlayerRecords.Take(xAmount).ToList();
    }
}
