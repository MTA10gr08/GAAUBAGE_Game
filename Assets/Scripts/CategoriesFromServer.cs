using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System.Collections;
using UnityEngine;

public class CategoriesFromServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        DontDestroyOnLoad(gameObject);
        StartCoroutine(GetCategories());
    }

    IEnumerator GetCategories() {
        var task = ConfigurationService.GetConfigurationAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        Configuration.categories = task.Result.Value;
        Destroy(gameObject);
    }
}
