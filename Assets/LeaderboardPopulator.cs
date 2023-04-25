using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPopulator : MonoBehaviour
{
    public GameObject RankedUserPrefab;
    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        //PopulateLeaderboardFromServer();
    }
    [ContextMenu("populate")]
    public void PopulateLeaderboardFromServer() {
        StartCoroutine(GetLeaderboard());
    }
    IEnumerator GetLeaderboard() {
        var task = LeaderboardService.GetLeaderboardAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(task.Result.ResultCode);
            yield break;
        }
        int index = 1;
        foreach (var entry in task.Result.Value.Entries) {
            var newEntry = Instantiate(RankedUserPrefab, gameObject.transform);
            newEntry.transform.SetSiblingIndex(index - 1);
            var user = newEntry.GetComponent<RankedUser>();
            user.name.text =  entry.Alias;
            user.rank.text =  ""+index;
            user.score.text =  ""+entry.Score;
            index++;
        }
        var currSpot = transform.GetChild(task.Result.Value.CurrentUserSpot);
        var currUser = currSpot.gameObject.GetComponent<RankedUser>();
        currUser.name.fontStyle = TMPro.FontStyles.Bold;
        currUser.rank.fontStyle = TMPro.FontStyles.Bold;
        currUser.score.fontStyle = TMPro.FontStyles.Bold;
    }
}
