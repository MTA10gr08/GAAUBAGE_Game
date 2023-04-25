using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPopulator : MonoBehaviour
{
    public GameObject RankedUserPrefab, loadingObject;
    List<GameObject> RankedUserList = new List<GameObject>();
    public int siblingIndexStart = -1;

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        PopulateLeaderboardFromServer();
    }
    [ContextMenu("populate")]
    public void PopulateLeaderboardFromServer() {
        StartCoroutine(GetLeaderboard());
    }
    IEnumerator GetLeaderboard() {
        loadingObject.SetActive(true);
        var task = LeaderboardService.GetLeaderboardAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(task.Result.ResultCode);
            yield break;
        }
        int index = 1;
        if (RankedUserList.Count !=0) {
            foreach (var user in RankedUserList) {
                Destroy(user);
            }
        }

        foreach (var entry in task.Result.Value.Entries) {
            var newEntry = Instantiate(RankedUserPrefab, gameObject.transform);
            newEntry.transform.SetSiblingIndex(index + siblingIndexStart);
            RankedUserList.Add(newEntry);
            var user = newEntry.GetComponent<RankedUser>();
            user.username.text = entry.Alias;
            user.rank.text = "" + index;
            user.score.text = "" + entry.Score;
            index++;
        }

        var currSpot = transform.GetChild(task.Result.Value.CurrentUserSpot);
        var currUser = currSpot.gameObject.GetComponent<RankedUser>();
        currUser.username.fontStyle = TMPro.FontStyles.Bold;
        currUser.rank.fontStyle = TMPro.FontStyles.Bold;
        currUser.score.fontStyle = TMPro.FontStyles.Bold;
        loadingObject.SetActive(false);
    }
}
