using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    [SerializeField]
    private List<Asset> assets = new List<Asset>();

    public static AssetManager Instance { get; private set; }

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

    public GameObject GetRandomAssetOfCategory(Asset.Category category)
    {
        var filteredList = assets.Where(asset => asset.category == category).ToList();
        return filteredList[Random.Range(0, filteredList.Count)].gameObject;
    }

    public GameObject GetAssetWithName(string name)
    {
        return assets.FirstOrDefault(asset => asset.name.Equals(name)).gameObject;
    }
}
