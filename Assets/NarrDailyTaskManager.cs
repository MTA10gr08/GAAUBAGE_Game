using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrDailyTaskManager : MonoBehaviour
{
    public GameObject NarrDailyTaskObject;
    public List<GameObject> DailyTasks;

    private void Start() {
        //depending on user level display all daily tasks they have access to
        //Get Dailytask DTO
        for (int i = 0; i < 3; i++) {
            var tmp = Instantiate(NarrDailyTaskObject, transform);
            tmp.transform.SetSiblingIndex(i);
            tmp.GetComponent<NarrativeProgressBarObject>().UpdateProgresBar(i*3, 10, "Daily task Number" + (i + 1));
            DailyTasks.Add(tmp);
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
