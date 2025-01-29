using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// Handles scene loading and transitions
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private float minimumLoadTime = 0.5f;
    
    private static SceneLoader instance;
    
    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SceneLoader");
                instance = go.AddComponent<SceneLoader>();
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

    public async Task LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;
        
        float startTime = Time.time;
        
        while (loadOperation.progress < 0.9f)
        {
            await Task.Yield();
        }
        
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < minimumLoadTime)
        {
            await Task.Delay((int)((minimumLoadTime - elapsedTime) * 1000));
        }
        
        loadOperation.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }
}