using GAAUBAGE_Game.API.Services;
using System.Collections;
using UnityEngine;

public class CategoriesFromServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
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
        Debug.Log(Configuration.categories.BackgroundCategories[0]);
        Destroy(gameObject);
    }
}
