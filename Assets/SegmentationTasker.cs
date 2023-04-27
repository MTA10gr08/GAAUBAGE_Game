using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using GAAUBAGE_Game.API.Models;

public class SegmentationTasker : MonoBehaviour
{
    SpriteFromURLSegmentation spriteFromURL;
    public Segmentation segmenter; 
    Guid currentID;
    public TMPro.TMP_Text taskText;

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
            Debug.Log(Endpoints.Segmentation.Next());
            yield break;
        }

        currentID = task.Result.Value.ID;
        taskText.text = task.Result.Value.TrashSubCategoriesConsensus.ToString();
        spriteFromURL.GetImageFromTask(task.Result.Value);
    }
    IEnumerator PostUserValues() {
        GAAUBAGE_Game.API.Models.Segmentation segmentation = new GAAUBAGE_Game.API.Models.Segmentation { SegmentationMultiPolygon =  segmenter.CompileMultiPolygon() };
        segmenter.ClearPolygon();
        var task = SegmentationService.PostSegmentationAsync(segmentation, currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(Endpoints.Segmentation.Post(currentID));
            yield break;
        }

        StartCoroutine(GetTask());
    }

}
