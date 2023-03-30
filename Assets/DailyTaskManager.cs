using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
    public GameObject DailyTaskObject;
    public List<GameObject> DailyTasks;

    private void Start() {
        //depending on user level display all daily tasks they have access to
        //Get Dailytask DTO
        for (int i = 0; i < 3; i++) {
            var tmp = Instantiate(DailyTaskObject, transform);
            tmp.GetComponent<ProgressBarObject>().UpdateProgresBar(3, 10, "Daily task Number" + (i + 1));
            DailyTasks.Add(tmp);
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
