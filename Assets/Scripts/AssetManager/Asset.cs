using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asset : MonoBehaviour
{
    public enum Category { FACE, HAIR, EYEBROW, EYE, NOSE, MOUTH, BEARD }

    public string name;
    public Category category;
}
