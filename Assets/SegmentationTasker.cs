using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using GAAUBAGE_Game.API.Models;
using UnityEngine.UI;

public class SegmentationTasker : MonoBehaviour
{
    SpriteFromURLSegmentation spriteFromURL;
    public Segmentation segmenter; 
    Guid currentID;
    public TMPro.TMP_Text taskText;
    public Button submitButton;

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        spriteFromURL = GetComponent<SpriteFromURLSegmentation>();
        StartCoroutine(GetTask());
    }

    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }
    IEnumerator GetTask() {
        spriteFromURL.LoadingObject.SetActive(true);
        var task = SegmentationService.NextSegmentationAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Destroy(segmenter);
            submitButton.interactable = false;
            spriteFromURL.ImageUnavailable();
            yield break;
        }

        (Guid, bool) test = (task.Result.Value.TrashSubCategoriesConsensus ?? task.Result.Value.ID, task.Result.Value.TrashSubCategoriesConsensus == null);
        var task2 = test.Item2 ? TrashSubCategoryService.GetTrashSubCategoryAsync(test.Item1) : TrashSubCategoryService.GetMyTrashSubCategoryAsync(test.Item1);

        yield return new WaitUntil(() => task2.IsCompleted);

        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            Destroy(segmenter);
            submitButton.interactable = false;
            spriteFromURL.ImageUnavailable();
            yield break;
        }

        currentID = task.Result.Value.ID;
        taskText.text = "Manipulate the shape to segment the" + task2.Result.Value.TrashSubCategoryLabel; //task.Result.Value.TrashSubCategoriesConsensus.ToString();
        spriteFromURL.GetImageFromTask(task.Result.Value);
    }
    IEnumerator PostUserValues() {
        GAAUBAGE_Game.API.Models.Segmentation segmentation = new GAAUBAGE_Game.API.Models.Segmentation { SegmentationMultiPolygon =  segmenter.CompileMultiPolygon() };

        var task = SegmentationService.PostSegmentationAsync(segmentation, currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(Endpoints.Segmentation.Post(currentID));
            if (task.Result.ResultCode == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError) {
                //No more Images in backlog
            }
            yield break;
        }
        segmenter.ClearPolygon();

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

        StartCoroutine(GetTask());
    }

}
