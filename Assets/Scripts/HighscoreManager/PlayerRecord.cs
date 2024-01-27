using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerRecord
{
    public string playerName;
    public int highScore;
    public List<FacialFeature> facialFeatures;
}

[System.Serializable]
public struct FacialFeature
{
    public string Name;
    public Transform Transform;
}