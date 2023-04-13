using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAndContextTasker : MonoBehaviour
{
    SpriteFromURL spriteFromURL;

    public BackgroundContextSelector bgSelector;
    //public ContextPopulator ctxPopulator;

    private void Awake() {
        spriteFromURL = GetComponent<SpriteFromURL>();
    }

    IEnumerator GetTask() {
        BackgroundClassification bgClass = new BackgroundClassification { };
        //var task = BackgroundClassificationService.NextBackgroundClassificationAsync(1);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
    }


}
