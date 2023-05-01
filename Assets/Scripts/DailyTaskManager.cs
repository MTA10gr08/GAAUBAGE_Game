using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
    public GameObject DailyTaskObject;
    public List<GameObject> DailyTasks;
    public LevelAccessController lac;
    public bool isNarrative = false;
    public int childIndexOffset = 0;


    private void Start() {
        StartCoroutine(GetTasksFromServer());
    }

    void PopulateTasks(List<UserGoal> tasks) {
        for (int i = 0; i <= Mathf.Clamp(lac.level, 0, 3); i++) {
            var tmp = Instantiate(DailyTaskObject, transform);
            tmp.transform.SetSiblingIndex(i + childIndexOffset);
            string Assignment;
            switch (tasks[i].TaskType) {
                case "CC":
                    Assignment = "Classify Image Contexts";
                    break;
                case "SI":
                    Assignment = "Make Sub-Images";
                    break;
                case "TC":
                    Assignment = "Assign Trash Categories";
                    break;
                case "Se":
                    Assignment = "Draw Segments";
                    break;
                default:
                    Assignment = "ERROR";
                    break;
            }
            if (isNarrative) {
                tmp.GetComponent<NarrativeProgressBarObject>().UpdateProgresBar((int)tasks[i].Done,
                                                                    (int)tasks[i].TotalToDo,
                                                                    Assignment);
            } else {
                tmp.GetComponent<ProgressBarObject>().UpdateProgresBar((int)tasks[i].Done,
                                                                    (int)tasks[i].TotalToDo,
                                                                    Assignment);
            }
            //tmp.GetComponent<ProgressBarObject>().UpdateProgresBar((int)tasks[i].Done,
            //                                                                (int)tasks[i].TotalToDo,
            //                                                                Assignment);

            DailyTasks.Add(tmp);
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    IEnumerator GetTasksFromServer() {
        var task = UserGoalService.GetUserGoalAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        PopulateTasks(task.Result.Value);
    }
}
