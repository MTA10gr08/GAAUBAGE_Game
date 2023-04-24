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
    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        spriteFromURL = GetComponent<SpriteFromURLSegmentation>();
        StartCoroutine(GetTask());
    }

    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }
    IEnumerator GetTask() {
        var task = SubImageGroupService.NextTrashBoundingBoxAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        currentID = task.Result.Value.ImageID;
        spriteFromURL.GetImageFromID(currentID);
    }
    IEnumerator PostUserValues() {
        Debug.Log("You sure did post those values");
        GAAUBAGE_Game.API.Models.Segmentation segmentation = new GAAUBAGE_Game.API.Models.Segmentation { SegmentationPolygon =  segmenter.CompileMultiPolygon() };
        //var task = SegmentationService.PostSegmentationAsync(segmentation, currentID);
        //yield return new WaitUntil(() => task.IsCompleted);
        //if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task.Result.ResponseCode);
        //    yield break;
        //}
        yield return null; //yeet when uncommented
        StartCoroutine(GetTask());
    }

}
