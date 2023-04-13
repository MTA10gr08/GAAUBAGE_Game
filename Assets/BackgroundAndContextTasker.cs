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

    Guid currentID;
    //public ContextPopulator ctxPopulator;

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        Debug.Log(APIRequestHandler.JWT);
        spriteFromURL = GetComponent<SpriteFromURL>();
        StartCoroutine(GetTask());
    }

    IEnumerator GetTask() {
        //BackgroundClassification bgClass = new BackgroundClassification { };
        var task = BackgroundClassificationService.NextBackgroundClassificationAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        spriteFromURL.LoadImage(task.Result.Value.URI);
        currentID = task.Result.Value.Id;

    }

    IEnumerator PostUserValues() {
        BackgroundClassification bgClass = new BackgroundClassification { Id = currentID};
        var task = BackgroundClassificationService.PostBackgroundClassificationAsync(bgClass);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        //ContextClassification ctxClass = new ContextClassification { Id = currentID };
        //var task2 = ContextClassificationService.PostContextClassificationAsync(ctxClass);
        //yield return new WaitUntil(() => task2.IsCompleted);
        //if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task2.Result.ResponseCode);
        //    yield break;
        //}
        StartCoroutine(GetTask());
    }


}
