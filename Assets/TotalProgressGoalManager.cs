using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalProgressGoalManager : MonoBehaviour
{
    public GameObject TotalProgressObject;

    private void Start() {
        //depending on user level display all daily tasks they have access to
        //Get Dailytask DTO
        var tmp = Instantiate(TotalProgressObject, transform);
        tmp.GetComponent<ProgressBarObject>().UpdateProgresBar(3, 100, "");
        DestroyImmediate(tmp.GetComponent<ProgressBarObject>().assignmentText.gameObject);
        tmp.GetComponent<ProgressBarObject>().progressText.text += "%"; 
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
    }
}
