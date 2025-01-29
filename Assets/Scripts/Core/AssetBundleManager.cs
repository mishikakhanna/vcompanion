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
            AssetBundle bundle = await LoadBundleAsync(bundlePath);
            if (bundle == null)
            {
                Debug.LogError($"Failed to load bundle: {bundleName}");
                return null;
            }
            loadedBundles[bundleName] = bundle;
        }

        T asset = await LoadAssetAsync<T>(loadedBundles[bundleName], assetName);
        return asset;
    }

    private async Task<AssetBundle> LoadBundleAsync(string bundlePath)
    {
        var bundleCreateRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        await Task.Run(() => { while (!bundleCreateRequest.isDone) ; });
        return bundleCreateRequest.assetBundle;
    }

    private async Task<T> LoadAssetAsync<T>(AssetBundle bundle, string assetName) where T : Object
    {
        var assetLoadRequest = bundle.LoadAssetAsync<T>(assetName);
        await Task.Run(() => { while (!assetLoadRequest.isDone) ; });
        return assetLoadRequest.asset as T;
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
