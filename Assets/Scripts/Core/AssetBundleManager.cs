using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Manages asset bundle loading and caching
/// </summary>
public class AssetBundleManager : MonoBehaviour
{
    private Dictionary<string, AssetBundle> loadedBundles = new Dictionary<string, AssetBundle>();
    private static AssetBundleManager instance;

    public static AssetBundleManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AssetBundleManager");
                instance = go.AddComponent<AssetBundleManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task<T> LoadAsset<T>(string bundleName, string assetName) where T : Object
    {
        if (!loadedBundles.ContainsKey(bundleName))
        {
            string bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, bundleName);
            var bundle = await AssetBundle.LoadFromFileAsync(bundlePath);
            if (bundle == null)
            {
                Debug.LogError($"Failed to load bundle: {bundleName}");
                return null;
            }
            loadedBundles[bundleName] = bundle;
        }

        var asset = await loadedBundles[bundleName].LoadAssetAsync<T>(assetName);
        return asset as T;
    }

    private void OnDestroy()
    {
        foreach (var bundle in loadedBundles.Values)
        {
            bundle.Unload(true);
        }
        loadedBundles.Clear();
    }
}