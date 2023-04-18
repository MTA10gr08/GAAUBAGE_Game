using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAndContextTasker : MonoBehaviour
{
    SpriteFromURL spriteFromURL;

    public BackgroundContextSelector bgSelector;
    public ContextSelector ctxSelector;

    Guid currentID;

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        Debug.Log(APIRequestHandler.JWT);
        spriteFromURL = GetComponent<SpriteFromURL>();
        StartCoroutine(GetTask());
    }

    IEnumerator GetTask() {
        var task = BackgroundClassificationService.NextBackgroundClassificationAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        spriteFromURL.LoadImage(task.Result.Value.URI);
        currentID = task.Result.Value.Id;

    }

    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }

    IEnumerator PostUserValues() {
        Debug.Log(currentID);
        BackgroundClassification bgClass = new BackgroundClassification { ImageId = currentID, BackgroundCategory = bgSelector.CompileStringList()[0]  };
        var task = BackgroundClassificationService.PostBackgroundClassificationAsync(bgClass);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        //ContextClassification ctxClass = new ContextClassification { BackgroundClassificationId = task.Result.Value.Id, Category = ctxSelector.SelectedContext};
        // var task2 = ContextClassificationService.PostContextClassificationAsync(ctxClass);
        //    yield return new WaitUntil(() => task2.IsCompleted);
        //    if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //        Debug.LogError(task2.Result.ResponseCode);
        //        yield break;
        //    }
        //    StartCoroutine(GetTask());
    }


}
