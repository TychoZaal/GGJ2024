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

    int maxHahaPerText = 4;

    public int currentScore = 0;

    [SerializeField]
    public List<Transform> hahaPlacements = new List<Transform>();

    public string playerName = "japie";

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

    public void calljatoch()
    {
        StartCoroutine(AssessCreatedCharacter());
    }

    public IEnumerator AssessCreatedCharacter()
    {
        currentScore += currentPlayerRecords.Sum(cpr => cpr.highScore);
        double average = currentPlayerRecords.Average(rec => rec.highScore);
        double success = average / ScoreCalculator.Instance.perfectScore;
        int hahaAmount = Mathf.CeilToInt((float)success * maxHahaPerText);
        int hahaPlaced = Mathf.CeilToInt((float)success * hahaPlacements.Count);

        for (int i = 0; i < hahaPlaced; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.3f));
            UIManager.Instance.SpawnText(string.Concat(Enumerable.Repeat("HA", hahaAmount)) + "!", hahaPlacements[i].transform);
        }

        Shuffle(hahaPlacements);
        currentPlayerRecords.Clear();
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

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            // Swap elements
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
