using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarrativeProgressBarObject : MonoBehaviour
{
    TMP_Text taskText;
    int maxChars = 20;
    string zero = " -", one = "█";
    private void Awake() {;
        taskText = GetComponent<TMP_Text>();
    }
    public void UpdateProgresBar(int value, int maxValue, string assignment) {
        taskText.text = assignment + "\n";
        taskText.text += "|";

        int proogress = Mathf.CeilToInt(maxChars / maxValue) * (value);
        for (int i = 0; i < maxChars; i++) {
            if (i <= proogress && proogress != 0) {
                taskText.text += one;
            } else {
                taskText.text += zero;
            }
        }
        taskText.text += "| ";
        taskText.text += value + " / " + maxValue;
    }
}
