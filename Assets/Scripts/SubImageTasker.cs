using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SubImageTasker : MonoBehaviour
{
    SpriteFromURL spriteFromURL;
    public BoxManager boxManager;
    Guid currentID;

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        spriteFromURL = GetComponent<SpriteFromURL>();
        StartCoroutine(GetTask());
    }

    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }
    IEnumerator GetTask() {
        spriteFromURL.LoadingObject.SetActive(true);
        var task = SubImageGroupService.NextTrashBoundingBoxAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            spriteFromURL.ImageUnavailable();
            boxManager.NewBoxBtn.interactable = false;
            boxManager.SubmitButton.interactable = false;

            yield break;
        }
        currentID = task.Result.Value.ID;
        spriteFromURL.GetImageFromID(task.Result.Value.Image);
    }
    IEnumerator PostUserValues() {
        spriteFromURL.LoadingObject.SetActive(true);
        SubImageAnnotationGroup trashBoxes = new SubImageAnnotationGroup { SubImageAnnotations = boxManager.ReturnBoxes()};
        var task = SubImageGroupService.PostTrashBoundingBoxAsync(trashBoxes, currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        boxManager.ClearBoxes();

        StartCoroutine(GetTask());

        var task3 = UserService.GetCurrentUserAsync();
        yield return new WaitUntil(() => task3.IsCompleted);
        if (task3.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task3.Result.ResponseCode);
            yield break;
        }

        if ((int)task3.Result.Value.Level != PlayerPrefs.GetInt("Level")) {
            PlayerPrefs.SetInt("Level", (int)task3.Result.Value.Level);

            if (PlayerPrefs.GetString("Tag") == "Narr") {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Narrative_Dialogue");
            } else {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("BLAP_LevelUp");
            }
            yield break;
        }
    }

    public void Skip() {
        StartCoroutine(SkipImageAnnotation());
    }

    IEnumerator SkipImageAnnotation() {
        var task = ImageAnnotationService.VoteSkipImageAnnotationAsync(currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(task.Result.ResultCode);
            yield break;
        }
        StartCoroutine(GetTask());
    }
}
