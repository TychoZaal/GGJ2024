using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    [SerializeField]
    private List<Asset> assets = new List<Asset>();

    private string nosePath = "Art/FaceParts/Noses";
    private string mouthPath = "Art/FaceParts/Mouths/";
    private string eyePath = "Art/FaceParts/Eyes/";

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

        AddFromPath(nosePath, Asset.Category.NOSE);
        AddFromPath(mouthPath, Asset.Category.MOUTH);
        AddFromPath(eyePath, Asset.Category.EYE);

    }

    private void AddFromPath(string resourcePath, Asset.Category cat)
    {
        var textures = Resources.LoadAll(resourcePath, typeof(Texture2D));
        foreach (var texture in textures)
        {
            //new asset with name, category, and texture
            Asset newAsset = new Asset();
            newAsset.name = texture.name;
            newAsset.category = cat;
            newAsset.texture = (Texture)texture;
            //add to list
            assets.Add(newAsset);
        }
    }

    public Texture GetRandomAssetOfCategory(Asset.Category category)
    {
        var filteredList = assets.Where(asset => asset.category == category).ToList();
        return filteredList[Random.Range(0, filteredList.Count)].texture;
    }

    public Texture GetAssetWithName(string name)
    {
        return assets.FirstOrDefault(asset => asset.name.Equals(name)).texture;
    }
}
