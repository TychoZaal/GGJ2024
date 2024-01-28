using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //list of materials
    [SerializeField]
    private List<Material> materials = new List<Material>();
    void Start()
    {
        //get the skin mesh renderer
        SkinnedMeshRenderer meshRenderer = GetComponent<SkinnedMeshRenderer>();
        //get a random material from the list
        Material material = materials[Random.Range(0, materials.Count)];
        //set the material
        meshRenderer.material = material;
    }
}
