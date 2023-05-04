using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPopulator : MonoBehaviour
{
    public TMPro.TMP_Text StatsText, CreatedText;
    public bool addUserName = true;


    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        PopulateStats();
    }
    [ContextMenu("Populate Stats")]
    void PopulateStats() {
        StartCoroutine(GetStatsFromServer());
    }

    IEnumerator GetStatsFromServer() {
        var payload = JWTReader.GetPayload(PlayerPrefs.GetString("JWT"));
        var task = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        StatsText.text = "";
        var user = task.Result.Value;
        if (addUserName) {
            StatsText.text += "Username:\t\t" + user.Alias + "\n";
        }
        StatsText.text += dateJoinedText(user.Created);
        if (addUserName) {
            StatsText.text += "Current Level:\t\t" + user.Level + "\n";
        } else {
            StatsText.text += "Access Level:\t\t" + user.Level + "\n";
        }
        StatsText.text += "Context Classifications:\t" + user.BackgroundClassifications.Count + " | " + user.BackgroundClassifications.Count * 1 + "p\n"
              + "Sub - Images made:\t\t" + user.SubImageGroups.Count + " | " + user.SubImageGroups.Count * 2 + "p\n"
              + "Categories assigned:\t\t" + user.TrashSuperCategories.Count + " | " + user.TrashSuperCategories.Count * 3 + "p\n"
              + "Segmentation drawn:\t\t" + user.Segmentations.Count + " | " + user.Segmentations.Count * 4 + "p\n\n"
              + "Daily tasks compleated:\t" + "0 | 0" + "p\n\n" //fix?
              + "Total Points:\t" + user.Score; //Fix?
        //Maybe score/leaderboardpos?
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        /*Context Classifications: 56 | 56p
        Sub - Images made: 30 | 45p
        Categories assigned: 13 | 26p
        Segmentation drawn: 10 | 30p

        Daily tasks compleated: 3 | 30p*/
    }
    string dateJoinedText(DateTime dateTime) {

        if (CreatedText != null) {
            CreatedText.GetComponent<TMPro.TMP_Text>().text = "Date Joined:\n" + dateTime.Date.ToString("dd/MM/yyyy");
            return "";
        }
        return "Date Joined:\t" + dateTime.Date.ToString("dd/MM/yyyy") + "\n";
    }
}
