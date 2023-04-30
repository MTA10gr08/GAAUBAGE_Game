using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAAUBAGE_Game.API.Services;

public class ComunityGoalManager : MonoBehaviour
{
    public GameObject progressObject;
    private GameObject ComunityGoalObject;
    public bool isNarrative = false;



    IEnumerator GetTasksFromServer() {
        var task = CommunityGoalService.GetCommunityGoalAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        if (true) {

        }
    }
}
