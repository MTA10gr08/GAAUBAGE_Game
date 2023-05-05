using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlapLevelController : MonoBehaviour
{
    public TMPro.TMP_Text LevelDescription, title;

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
        title.text = "Congratulations! You have leveled up!";
        switch (task.Result.Value.Level) {
            case 1:
                LevelDescription.text = "Here at Level 1, you get access to a slightly more difficult task: Making Sub-Images. In this task, you need to inspect images and draw boxes around each piece of garbage. Do not worry about being precise, it is preferable to leave a little space on all sides of the garbage. Each image might have more than one piece of trash, try and mark as many as you can!";

                break;
            case 2:
                LevelDescription.text = "Welcome to Level 2! You have again earned yourself access to a new task: Category Assignment. This task will involve categorizing individual pieces of garbage into their specific category. Do your best, there are a lot of categories to choose from.";
                break;
            case 3:
                LevelDescription.text = "You have reached level 3 and have therefore unlocked the final type of task: Drawing Segmentations. Use the tool on the screen to manipulate the shape to fit the edges of the specified garbage. Pressing the outline will add a new point to make the segmentation more precise. Tapping a point will remove it. Try and be as precise as possible.";
                break;
            case 4:
                LevelDescription.text = "You have reached the final level. There is no new task for you. Thank you for using our app. Your contributions are very appreciated and have helped populate our dataset. We hope you will continue to do so.";
                break;
            default:
                title.text = "Welcome to our App";
                LevelDescription.text = "Welcome and thanks for downloading our app. By playing it you help contribute to a dataset we are making using images of litter. You will start with access to one task; litter context and background tagging. Each image needs to be tagged with whether or not it contains litter, and what the background of the image contains, like roads or vegetation. As you play you will unlock new and more difficult tasks.";
                break;
        }
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
