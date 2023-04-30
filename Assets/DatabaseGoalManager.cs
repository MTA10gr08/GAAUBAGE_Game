using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseGoalManager : MonoBehaviour
{
    public GameObject progressObject;
    private GameObject DatasetGoalObject;
    public bool isNarrative = false;
    public int childIndexOffset = 0;

    private void Start() {
        StartCoroutine(GetGoalFromServer());
    }

    IEnumerator GetGoalFromServer() {
        var task = DatabaseInfoService.GetUserGoalAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        string Assignment = "Dataset completion:";
        var value = task.Result.Value;
        DatasetGoalObject = Instantiate(progressObject, transform);
        DatasetGoalObject.transform.SetSiblingIndex(childIndexOffset);
        if (isNarrative) {
            DatasetGoalObject.GetComponent<NarrativeProgressBarObject>().UpdateProgresBar((int)value.TotalSegmentated,
                                                                1000,
                                                                Assignment);
        } else {
            DatasetGoalObject.GetComponent<ProgressBarObject>().UpdateProgresBar((int)value.TotalSegmentated,
                                                                1000,
                                                                Assignment);
        }
    }
}
