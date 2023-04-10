using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Annotatinator2 : MonoBehaviour
{
    public SpriteFromURL imageObject;
    public SegmentContainer segmentContainerPrefab;
    private SegmentContainer currentSegmentContainer = null;

    public Button submitButton;
    public TMPro.TMP_Text taskText;

    public string task = "Task 1";

    private void Start() {
        //get image from URL
        imageObject.LoadTestUrl();
        //write task
        if (taskText != null) {
            taskText.text = "This is your task: " + task;
        }
        //instantiate segment
        currentSegmentContainer = Instantiate(segmentContainerPrefab, imageObject.transform);
        //submit segment
        Debug.Log(currentSegmentContainer.name);
    }
}
