using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAAUBAGE_Game.API.Services;

public class ComunityGoalManager : MonoBehaviour
{
    public GameObject progressObject;
    private GameObject ComunityGoalObject;
    public bool isNarrative = false;
    public int childIndexOffset = 0;
    public TMPro.TMP_Text personalContribution;

    private void Start() {
        personalContribution.text = "";
        StartCoroutine(GetGoalFromServer());
    }

    IEnumerator GetGoalFromServer() {
        var task = CommunityGoalService.GetCommunityGoalAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        string Assignment = string.Empty;
        var value = task.Result.Value;
        switch (task.Result.Value.TaskType) {
            case "CC":
                Assignment += "Classify Image Contexts";
                break;
            case "SI":
                Assignment += "Make Sub-Images";
                break;
            case "TC":
                Assignment += "Assign Trash Categories";
                break;
            case "Se":
                Assignment += "Draw Segments";
                break;
            default:
                Assignment = "ERROR";
                break;
        }
        ComunityGoalObject = Instantiate(progressObject, transform);
        ComunityGoalObject.transform.SetSiblingIndex(childIndexOffset);
        if (isNarrative) {
            ComunityGoalObject.GetComponent<NarrativeProgressBarObject>().UpdateProgresBar((int)value.DoneAll,
                                                                (int)value.TotalToDo,
                                                                Assignment);
        } else {
            ComunityGoalObject.GetComponent<ProgressBarObject>().UpdateProgresBar((int)value.DoneAll,
                                                                (int)value.TotalToDo,
                                                                Assignment);
        }

        if (personalContribution != null) {
            personalContribution.text = "Personal Contribution: " + (int)value.DoneYou;
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
