using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReJWT : MonoBehaviour
{
    [ContextMenu("JWT mig i munden")]
    void ReJwter() {
        StartCoroutine(UsernameCreate());
    }

    IEnumerator UsernameCreate() {
        PlayerPrefs.SetString("Alias", "TESTUSER");
        User user = new User { Alias = PlayerPrefs.GetString("Alias") };
        var task = UserService.PostUserAsync(user);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        PlayerPrefs.SetString("JWT", task.Result.Value);
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        var payload = JWTReader.GetPayload(PlayerPrefs.GetString("JWT"));
        var task2 = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task2.IsCompleted);

        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }
        Debug.Log(task2.Result.Value.ID);
    }
}
