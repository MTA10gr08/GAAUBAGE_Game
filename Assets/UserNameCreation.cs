using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Services;
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

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("jwt"))) {
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

        if (task.Result.ResultCode == UnityEngine.Networking.UnityWebRequest.Result.Success) {
            PlayerPrefs.SetString("jwt", task.Result.Value);
            Debug.Log(task.Result.Value);
            Debug.Log(PlayerPrefs.GetString("jwt"));

        } else Debug.LogError(task.Result.ResponseCode);

        //get token tag
        //based on token tag loade scene; for now loade blap
        SceneManager.LoadSceneAsync("BLAP_Home");
    }
}
