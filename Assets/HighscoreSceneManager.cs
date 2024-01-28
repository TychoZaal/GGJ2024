using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreSceneManager : MonoBehaviour
{
    public TextMeshProUGUI overview, title;

    public List<Color> colorPallette = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        overview.SetText("");

        var highscores = HighscoreManager._instance.LoadHighscores(10);
        string joepie = $"", joepie2 = $"", joepie3 = $"";
        int counter = 1;

        foreach (var score in highscores)
        {
            joepie += counter + "." + score.playerName + ": " + score.highScore + " laughs! \n";
            counter++;
        }

        foreach (char c in joepie)
        {
            Color color = colorPallette[Random.Range(0, colorPallette.Count)];
            joepie2 += $"{c.ToString().AddColor(color)}";
        }

        foreach (char c in title.text)
        {
            Color color = colorPallette[Random.Range(0, colorPallette.Count)];
            joepie3 += $"{c.ToString().AddColor(color)}";
        }

        overview.SetText(joepie2);
        title.SetText(joepie3);
    }
}
