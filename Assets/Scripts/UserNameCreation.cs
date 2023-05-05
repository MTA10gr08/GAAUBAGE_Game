using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserNameCreation : MonoBehaviour
{
    bool submitting = false;
    public GameObject UsernameContent;
    public TMPro.TMP_Text usernameField, errorText;
    public Button submitBtn;

    private void Awake() {

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("JWT"))) {
           
            UsernameContent.SetActive(true);
        } else {
            APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
            StartCoroutine(CrossCheckUser());
        }
    }
    IEnumerator CrossCheckUser() {
        var task = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            UsernameContent.SetActive(true);

            yield break;
        }

        UsernameContent.SetActive(false);
        Debug.Log("Wack");
        //load a different scene dependent on the user tag read from thier tag
        string home = PlayerPrefs.GetString("Tag") == "Blap" ? "BLAP_Home" : "Narrative_Home";
        SceneManager.LoadSceneAsync(home);
    }
    public void submitUsernameToServer() {
        submitBtn.interactable = false;

        string pattern = @"^(?=.*[^\s])[a-zA-Z\s\p{L}]{5,15}$";

        Regex regex = new Regex(pattern);
        bool result = regex.IsMatch(usernameField.text);
        if (result == false) {
            errorText.gameObject.SetActive(true);
            errorText.text = "*Username can not be empty and must be between 5 and 15 characters.";
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            submitBtn.interactable = true;
            return;
        }
        if (submitting != true) {
            submitting = true;
            errorText.gameObject.SetActive(false);
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
            submitting = false;
            submitBtn.interactable = true;
            errorText.gameObject.SetActive(true);
            errorText.text = "*Server Error: Please contact the developer.";
            yield break;
        }

        PlayerPrefs.SetString("JWT", task.Result.Value);
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");

        //get token tag
        var task2 = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task2.IsCompleted);

        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }

        PlayerPrefs.SetString("Tag", task2.Result.Value.Tag);
        PlayerPrefs.SetInt("Level", (int)task2.Result.Value.Level);
        if (PlayerPrefs.GetString("Tag").Equals("Narr")) {
            SceneManager.LoadSceneAsync("Narrative_Dialogue");
        } else {
            //SceneManager.LoadSceneAsync("BLAP_Home");
            SceneManager.LoadSceneAsync("BLAP_LevelUp");
        }
    }
}
