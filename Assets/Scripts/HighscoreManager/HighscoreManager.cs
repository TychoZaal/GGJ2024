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
    public static HighscoreManager Instance { get; private set; }

    public List<PlayerRecord> playerRecords = new List<PlayerRecord>();

    [SerializeField]
    private Transform face, eyeLeft, eyeRight, mouth;

    private string fileName = "highscores.csv";

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

    // Start is called before the first frame update
    void Start()
    {
        AddScore(new PlayerRecord
        {
            playerName = "Tycho",
            highScore = UnityEngine.Random.Range(10, 1000),
            facialFeatures = new List<FacialFeature>
            {
                new FacialFeature
                {
                    Name = "Face",
                    Transform = face.transform
                },
                new FacialFeature
                {
                    Name = "EyeLeft",
                    Transform = eyeLeft.transform
                },
                new FacialFeature
                {
                    Name = "EyeRight",
                    Transform = eyeLeft.transform
                },
                new FacialFeature
                {
                    Name = "Mouth",
                    Transform = eyeLeft.transform
                }
            }
        });

        AddScore(new PlayerRecord
        {
            playerName = "Tycho",
            highScore = UnityEngine.Random.Range(10, 1000),
            facialFeatures = new List<FacialFeature>
            {
                new FacialFeature
                {
                    Name = "Face",
                    Transform = face.transform
                },
                new FacialFeature
                {
                    Name = "EyeLeft",
                    Transform = eyeLeft.transform
                },
                new FacialFeature
                {
                    Name = "EyeRight",
                    Transform = eyeLeft.transform
                },
                new FacialFeature
                {
                    Name = "Mouth",
                    Transform = eyeLeft.transform
                }
            }
        });

        AddScore(new PlayerRecord
        {
            playerName = "Tycho",
            highScore = UnityEngine.Random.Range(10, 1000),
            facialFeatures = new List<FacialFeature>
            {
                new FacialFeature
                {
                    Name = "Face",
                    Transform = face.transform
                },
                new FacialFeature
                {
                    Name = "EyeLeft",
                    Transform = eyeLeft.transform
                },
                new FacialFeature
                {
                    Name = "EyeRight",
                    Transform = eyeLeft.transform
                },
                new FacialFeature
                {
                    Name = "Mouth",
                    Transform = eyeLeft.transform
                }
            }
        });

        StoreHighScore();
    }

    public void AddScore(PlayerRecord playerRecord)
    {
        playerRecords.Add(playerRecord);
    }

    public void StoreHighScore()
    {
        var playerRecord = playerRecords.OrderByDescending(playerRecord => playerRecord.highScore).ToList().FirstOrDefault();

        var sb = new StringBuilder();
        sb.Append(playerRecord.playerName + ";");
        sb.Append(playerRecord.highScore + ";");
        sb.Append("[");

        foreach (var facialFeature in playerRecord.facialFeatures)
        {
            sb.Append(facialFeature.Name + ":");
            sb.Append(facialFeature.Transform.position + ","
                + facialFeature.Transform.rotation + ","
                + facialFeature.Transform.localScale + "-");
        }

        sb.Append("]");
        sb.AppendLine(";");

        File.AppendAllText(fileName, sb.ToString());

        Debug.LogError("Done storing highscore");
    }

    public void LoadData()
    {

    }
}
