using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrDailyTaskManager : MonoBehaviour
{
    public GameObject NarrDailyTaskObject;
    public List<GameObject> DailyTasks;
    public LevelAccessController lac;

    private void Start() {
        StartCoroutine(GetTasksFromServer());
    }

    void PopulateTasks (List<UserGoal> tasks) {
        for (int i = 0; i <= lac.level; i++) {
            var tmp = Instantiate(NarrDailyTaskObject, transform);
            tmp.transform.SetSiblingIndex(i);
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
            tmp.GetComponent<NarrativeProgressBarObject>().UpdateProgresBar((int)tasks[i].Done, 
                                                                            (int)tasks[i].TotalToDo, 
                                                                            Assignment);

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
