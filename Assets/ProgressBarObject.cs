using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBarObject : MonoBehaviour
{
    public TMP_Text progressText, assignmentText;
    public Slider progressBar, personalProgressBar;

    public void UpdateProgresBar(float value, float maxValue, string assignment) {
        progressBar.maxValue = maxValue;
        progressBar.value = value;
        assignmentText.text = assignment;
        progressText.text = progressBar.value + " / " + progressBar.maxValue;
    }
    public void UpdatePersonalProgressbar(float value, float maxValue) {

        if (!personalProgressBar) {
            return;
        }
        personalProgressBar.maxValue = maxValue;
        personalProgressBar.value = value;
    }
}
