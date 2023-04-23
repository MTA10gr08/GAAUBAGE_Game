using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarrativeProgressBarObject : MonoBehaviour
{
    public TMP_Text taskText;
    string assignmentText = "TASK 1: xxxxxxxxxx";
    public int curr = 7, max = 10;

    int maxChars = 20;
    string zero = " -", one = "█";
    private void Awake() {
        taskText.text = assignmentText + "\n";
        taskText.text += "|";

        int proogress =  Mathf.CeilToInt(maxChars / max) * curr;
        Debug.Log(proogress);
        for (int i = 0; i < maxChars; i++) {
            if (i <= proogress && proogress != 0) {
                taskText.text += one;
            } else {
                taskText.text += zero;
            }
        }
        taskText.text += "| ";
        taskText.text += curr + " / " + max;
    }
    public void UpdateProgresBar(float value, float maxValue, string assignment) {
        taskText.text = assignmentText + "\n";
        taskText.text += "|";

        int proogress = Mathf.CeilToInt(maxChars / max) * curr;
        Debug.Log(proogress);
        for (int i = 0; i < maxChars; i++) {
            if (i <= proogress && proogress != 0) {
                taskText.text += one;
            } else {
                taskText.text += zero;
            }
        }
        taskText.text += "| ";
        taskText.text += curr + " / " + max;
    }
}
