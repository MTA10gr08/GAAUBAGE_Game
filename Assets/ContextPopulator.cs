using API.DTOs.Annotation;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;

public class ContextPopulator : MonoBehaviour
{
    private void Awake() {
        var user = new UserDTO { Alias = "Marcusi boy", Tag = "nerd" };
        var userSerialized = JsonUtility.ToJson(user);

        var postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        var formData = System.Text.Encoding.UTF8.GetBytes(userSerialized);

        //var www = new WWW("https://localhost:5255/", formData, postHeader);

        //StartCoroutine(WaitForRequest(www));

        //www.responseHeaders.TryGetValue("application/json", out var value);
        //Debug.Log(value);

        UnityWebRequest unityWebRequest = new UnityWebRequest();
        unityWebRequest.uri = new System.Uri("https://localhost:7000/");
        var request = UnityWebRequest.PostWwwForm("https://localhost:7000/user/", userSerialized);
        var response = request.SendWebRequest();
        response.completed += delegate { Debug.Log(request.result.ToString()); };

    }

    private IEnumerator WaitForRequest(WWW www) {
        yield return www;

        if (www.error == null) {
            Debug.Log("WWW Ok:" + www.text);
        } else {
            Debug.Log("WWW Error:" + www.error);
        }
    }
}
