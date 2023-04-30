using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UserNameCreation : MonoBehaviour
{
    bool submitting = false;
    public GameObject UsernameContent;
    public TMPro.TMP_Text usernameField;

    private void Awake() {

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("JWT"))) {
            //load a different scene dependent on the user tag read from thier token
            //But for now just load BLAP
            UsernameContent.SetActive(true);
        } else if (string.IsNullOrEmpty(PlayerPrefs.GetString("Tag"))) {

        } else {
            UsernameContent.SetActive(false);
            string home = PlayerPrefs.GetString("Tag") == "Blap" ? "BLAP_Home" : "Narrative_Home";
            SceneManager.LoadSceneAsync(home);
        }

    }
    public void submitUsernameToServer() {
        //User user = new User { Alias = usernameField.text };
        //UserService.PostUser(user, (response) => {
        //    if (response.ResultCode == UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //        PlayerPrefs.SetString("jwt", response.Value);

        //    } else Debug.LogError(response.ToString());
        //});
        if (submitting != true) {
            submitting = true;
            StartCoroutine(UsernameCreate());
        }
    }
    IEnumerator UsernameCreate() {
        PlayerPrefs.SetString("Alias", usernameField.text);
        User user = new User { Alias = usernameField.text };
        var task = UserService.PostUserAsync(user);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        PlayerPrefs.SetString("JWT", task.Result.Value);
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        //get token tag
        var task2 = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task2.IsCompleted);
        string tag = "";

        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }


        tag = task2.Result.Value.Tag;
        PlayerPrefs.SetString("Tag", tag);

        if (tag.Equals("Narr")) {
            SceneManager.LoadSceneAsync("Narrative_Dialogue");
        } else {
            SceneManager.LoadSceneAsync("BLAP_LevelUp");
        } 
    }
}
