using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UserNameCreation : MonoBehaviour
{
    public bool UserHasToken = false;
    public GameObject UsernameContent;
    public TMPro.TMP_Text usernameField;

    private void Awake() {

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("JWT"))) {
            //load a different scene dependent on the user tag read from thier token
            //But for now just load BLAP
            UsernameContent.SetActive(true);
        } else {
            SceneManager.LoadSceneAsync("BLAP_Home");
        }

    }
    public void submitUsernameToServer() {
        //User user = new User { Alias = usernameField.text };
        //UserService.PostUser(user, (response) => {
        //    if (response.ResultCode == UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //        PlayerPrefs.SetString("jwt", response.Value);

        //    } else Debug.LogError(response.ToString());
        //});

        StartCoroutine(UsernameCreate());
    }
    IEnumerator UsernameCreate() {
        User user = new User { Alias = usernameField.text };
        var task = UserService.PostUserAsync(user);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        PlayerPrefs.SetString("JWT", task.Result.Value);
        Debug.Log(task.Result.Value);
        Debug.Log(PlayerPrefs.GetString("JWT"));

        //get token tag
        var payload = JWTReader.GetPayload(PlayerPrefs.GetString("JWT"));
        var task2 = UserService.GetUserAsync(Guid.Parse(payload.nameid));
        yield return new WaitUntil(() => task2.IsCompleted);
        string tag = "";

        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }

        tag = task2.Result.Value.Tag;

        if (tag.Equals("BLAP")) {
            SceneManager.LoadSceneAsync("BLAP_Home");
        } else if (tag.Equals("Narative")) {
            SceneManager.LoadSceneAsync("Narrative_Home");
        } else {
            Debug.LogError("No Valid Tag");
            yield break;
        }
    }
}
