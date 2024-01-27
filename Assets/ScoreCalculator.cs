using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    //the width of the face is important for calculating the score of the eyes, eyebrows etc.
    public float faceWidth = 1.0f;
    public Vector2 faceCenter = Vector2.zero;
    public float perfectScore = 1000.0f;
    //Given a category, calculate the score for that category
    //for categories with two parts, the given position is the center of the two parts
    public float calculateScoreSingle(Vector2 pos)
    { 
        //get the distance from the center of the face
        float distance = Vector2.Distance(pos, faceCenter);

        //if the distance is greater than the width of the face, return 0
        if (distance > faceWidth)
        {
            return 0.0f;
        }

        //otherwise, return the score based on the distance
        //the closer the distance, the higher the score (up to perfectScore)
        return perfectScore * (1.0f - (distance / (faceWidth / 2)));
    }

    public float calculateScorePair(Vector2 first, Vector2 second)
    {
        //get the position between the two parts
        Vector2 posBetween = (first + second) / 2;

        //check if each part is within the face
        float firstDistance = Vector2.Distance(first, faceCenter);
        float secondDistance = Vector2.Distance(second, faceCenter);

        //if either part is outside the face, return 0
        if (firstDistance > faceWidth / 2 || secondDistance > faceWidth / 2)
        {
            return 0.0f;
        }

        //get the distance from the center of the face
        float distance = Vector2.Distance(posBetween, faceCenter);
        //get the ratio of the distance to the width of the face
        float ratio = distance / (faceWidth / 2);

        return perfectScore * (1.0f - ratio);
    }

}
