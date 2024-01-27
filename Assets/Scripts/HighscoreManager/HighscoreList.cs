using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Highscore List", menuName = "Highscores")]
public class HighscoreList : ScriptableObject
{
    public List<PlayerRecord> highScores = new List<PlayerRecord>();
}
