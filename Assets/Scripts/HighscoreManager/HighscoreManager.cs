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
    public List<PlayerRecord> allCurrentPlayerRecords = new List<PlayerRecord>();

    public HighscoreList highScoreList;

    int maxHahaPerText = 4;

    public int currentScore = 0;

    [SerializeField]
    public List<Transform> hahaPlacements = new List<Transform>();

    public List<AudioClip> hahaClips = new List<AudioClip>();

    int hahaAmount = 0, hahaPlaced = 0, hahaClip = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

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
        allCurrentPlayerRecords.Add(playerRecord);
    }

    public int GetHighscore()
    {
        return currentScore;
    }

    public IEnumerator AssessCreatedCharacter()
    {
        AudioClip clip = hahaClips[hahaClip];

        for (int i = 0; i < hahaPlaced; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.3f));
            UIManager.Instance.SpawnText(string.Concat(Enumerable.Repeat("HA", hahaAmount)) + "!", hahaPlacements[i].transform, UIManager.Instance.hahaColors);
        }
        SoundManager._instance.PlaySound(hahaPlacements[hahaPlaced], clip, 1.0f, 0.4f);

        hahaAmount = 0;
        hahaPlaced = 0;
        hahaClip = 0;
        Shuffle(hahaPlacements);
    }

    public void CalculateCreatedCharacterScore()
    {
        currentScore += currentPlayerRecords.Sum(cpr => cpr.highScore);
        double average = currentPlayerRecords.Average(rec => rec.highScore);
        double success = average / ScoreCalculator.Instance.perfectScore;
        hahaAmount = Mathf.CeilToInt((float)success * maxHahaPerText) - 1;
        hahaPlaced = Mathf.CeilToInt((float)success * hahaPlacements.Count) - 1;
        hahaClip = Mathf.CeilToInt((float)success * hahaClips.Count) - 1;

        currentPlayerRecords.Clear();
    }

    public void StoreHighestScore()
    {
        highScoreList.highScores.Add(new PlayerRecord
        {
            highScore = allCurrentPlayerRecords.Sum(pr => pr.highScore),
            playerName = StartGameManager.Instance.playerName
        });
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
