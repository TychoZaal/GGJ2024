using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighscoreDummyFeeder : MonoBehaviour
{
    [SerializeField]
    private GameObject decalProjector;

    // Start is called before the first frame update
    void Start()
    {
        //HighscoreManager._instance.AddScore(new PlayerRecord
        //{
        //    playerName = "Tycho",
        //    highScore = Random.Range(10, 1000),
        //    facialFeatures = new List<FacialFeature>
        //    {
        //        new FacialFeature
        //        {
        //            Name = "eyeSphere",
        //            Position = eye.position
        //        },
        //        new FacialFeature
        //        {
        //            Name = "eyeSphere",
        //            Position = eye2.position
        //        },
        //        new FacialFeature
        //        {
        //            Name = "faceSphere",
        //            Position = face.position
        //        },
        //        new FacialFeature
        //        {
        //            Name = "mouthSphere",
        //            Position = mouth.position
        //        }
        //    }
        //});

        //HighscoreManager._instance.StoreHighestScore();

        PlayerRecord topScore = HighscoreManager._instance.LoadHighscores(1).FirstOrDefault();

        foreach (FacialFeature facialFeature in topScore.facialFeatures)
        {
            GameObject decal = Instantiate(decalProjector, facialFeature.Position, Quaternion.identity, null);
        }
    }
}
