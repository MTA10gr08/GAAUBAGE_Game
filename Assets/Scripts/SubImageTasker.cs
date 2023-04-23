using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubImageTasker : MonoBehaviour
{
    SpriteFromURLSegmentation spriteFromURL;
    public CategoryPopulator categoryPopulator;
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
        var task = TrashBoundingBoxService.NextTrashBoundingBoxAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
    }
    IEnumerator PostUserValues() {
        Debug.Log("You sure did post those values"); //Skal jeg efterlade ImageAnnotation tom? eller hvad skal i den?
       // TrashBoundingBox sCategory = new TrashBoundingBox { BoundingBoxs =  };
        //var task = TrashSuperCategoryService.PostTrashSuperCategoryAsync(sCategory, currentID);
        //yield return new WaitUntil(() => task.IsCompleted);
        //if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task.Result.ResponseCode);
        //    yield break;
        //}
        yield return null;
        StartCoroutine(GetTask());
    }

}
