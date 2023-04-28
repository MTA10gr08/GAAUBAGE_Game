using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAccessController : MonoBehaviour
{
    //Level needs to be adjusted by server somehow
    public int level;
    public List<Button> taskButtons = new List<Button>();
    void Awake() {
        StartCoroutine(GetLevelFromServer());
    }

    IEnumerator GetLevelFromServer() {
        var payload = JWTReader.GetPayload(PlayerPrefs.GetString("JWT"));
        var task = UserService.GetUserAsync(Guid.Parse(payload.nameid));
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        level = (int)task.Result.Value.Level;
        PlayerPrefs.SetInt("Level", level);
        for (int i = 0; i < taskButtons.Count; i++) {

            if (level >= i) {
                taskButtons[i].interactable = true;
            } else {
                taskButtons[i].interactable = false;
            }

        }

    }
}
