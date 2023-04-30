using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlapLevelController : MonoBehaviour
{
    public TMPro.TMP_Text LevelDescription;

    private void Awake() {
        StartCoroutine(GetLevelFromServer());
    }
    IEnumerator GetLevelFromServer() {
        var task = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        switch (task.Result.Value.Level) {
            case 1:
                LevelDescription.text = "At Level 1 you get access to a slightly more difficult task: Making Sub-Images. " +
                                        "In this task you need to inspect images and draw boxes around each piece of garbage. " +
                                        "Do not worry about being precise, it is preferable to leave a little space on all sides of the garbage. " +
                                        "Each image might have more than one piece of trash, try and mark as many as you can!";

                break;
            case 2:
                LevelDescription.text = "Welcome to Level 2! You have again earned yourself access to a new task: Category Assignment. " +
                                        "This task will involve categorizing individual pieces of garbage into its specefic category. " +
                                        "Do your best, there are a lot of categories to choose from.";
                break;
            case 3:
                LevelDescription.text = "You have reached level 3 and have therefor unlocked the final type of tasks: Drawing Segmentations. " +
                                        "Use the tool on screen to manipulate the shape to fit the edges of specified garbage. " +
                                        "Pressing the outline will add a new point to make your making more precise. Tapping a handle will remove the point." +
                                        "Try and be as precise as possible.";
                break;
            case 4:
                LevelDescription.text = "";
                break;
            default:
                LevelDescription.text = "Welcome to the app"; //maybe
                break;
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
