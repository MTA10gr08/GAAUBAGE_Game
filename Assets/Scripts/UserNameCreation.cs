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
using Unity.Notifications.Android;

public class UserNameCreation : MonoBehaviour
{

    bool submitting = false;
    public GameObject UsernameContent;
    public TMPro.TMP_Text usernameField, errorText;
    public Button submitBtn;

    private void Awake()
    {
        PlayerPrefs.SetInt("notificationID", -10);
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("JWT")))
        {

            UsernameContent.SetActive(true);
            AndroidNotificationCenter.CancelAllNotifications();
        }
        else
        {
            APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
            StartCoroutine(CrossCheckUser());
        }
    }
    IEnumerator CrossCheckUser()
    {
        var task = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        Debug.Log(task.Result != null);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            Debug.LogError(task.Result.ResponseCode);
            UsernameContent.SetActive(true);
            AndroidNotificationCenter.CancelAllNotifications();

            yield break;
        }

        PlayerPrefs.SetString("Tag", task.Result.Value.Tag);
        UsernameContent.SetActive(false);
        Debug.Log("Wack");
        //load a different scene dependent on the user tag read from thier tag
        string home = PlayerPrefs.GetString("Tag") == "Blap" ? "BLAP_Home" : "Narrative_Home";
        SceneManager.LoadSceneAsync(home);
    }
    private static readonly Regex UsernameRegex = new Regex(@"^\S(?:(?!\s{2,}).){3,15}$", RegexOptions.Compiled);
    public void submitUsernameToServer()
    {

        var username = usernameField.text;
        if (!UsernameRegex.IsMatch(username))
        {
            errorText.gameObject.SetActive(true);
            errorText.text = (username.Length < 4 || username.Length > 15) ? "*Username must be between 4 and 15 characters."
                : "*Username must not have consecutive spaces and must start and end with a non-space character.";
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            usernameField.text = Regex.Replace(username, @"\s+", " ").Trim();
            submitBtn.interactable = true;
        }
        else if (submitting != true)
        {
            submitting = true;
            errorText.gameObject.SetActive(false);
            StartCoroutine(UsernameCreate());
        }
    }
    IEnumerator UsernameCreate()
    {
        PlayerPrefs.SetString("Alias", usernameField.text);
        User user = new User { Alias = usernameField.text };
        var task = UserService.PostUserAsync(user);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
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

        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }

        PlayerPrefs.SetString("Tag", task2.Result.Value.Tag);
        PlayerPrefs.SetInt("Level", (int)task2.Result.Value.Level);
        if (PlayerPrefs.GetString("Tag").Equals("Narr"))
        {
            SceneManager.LoadSceneAsync("Narrative_Dialogue");
        }
        else
        {
            //SceneManager.LoadSceneAsync("BLAP_Home");
            SceneManager.LoadSceneAsync("BLAP_LevelUp");
        }
    }
}
