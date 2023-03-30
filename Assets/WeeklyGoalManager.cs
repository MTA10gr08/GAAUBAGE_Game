using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyGoalManager : MonoBehaviour
{
    public GameObject WeeklyGoalObject;
    public TMPro.TMP_Text personalContribution;

    private void Start() {
        //depending on user level display all daily tasks they have access to
        //Get Dailytask DTO
        var tmp = Instantiate(WeeklyGoalObject, transform);
        tmp.transform.SetSiblingIndex(1);
        tmp.GetComponent<ProgressBarObject>().UpdateProgresBar(5, 10, "Weekly Goal");
        tmp.GetComponent<ProgressBarObject>().UpdatePersonalProgressbar(1, 10);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        personalContribution.text = "Personal Contribution: " + 1;
    }

}
