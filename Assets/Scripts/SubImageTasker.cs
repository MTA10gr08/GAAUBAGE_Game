using Assets.Scripts.API.Models;
using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubImageTasker : MonoBehaviour
{
    SpriteFromURLSegmentation spriteFromURL;
    public BoxManager boxManager;
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
        //ImageAnnotation image = new ImageAnnotation();
        //var task2 = ContextClassificationService.GetContextClassification();
        currentID = task.Result.Value.Id;
        spriteFromURL.GetImageFromID(task.Result.Value.ImageID);
    }
    IEnumerator PostUserValues() {
        Debug.Log("You sure did post those values");
        SubImageGroup trashBoxes = new SubImageGroup { SubImages =  boxManager.ReturnBoxes()};
        var task = SubImageGroupService.PostTrashBoundingBoxAsync(trashBoxes, currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        StartCoroutine(GetTask());
    }

}
