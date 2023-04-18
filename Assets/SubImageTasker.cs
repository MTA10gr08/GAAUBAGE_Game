using GAAUBAGE_Game.API.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubImageTasker : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTask());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetTask() {
        var task = TrashBoundingBoxService.NextTrashBoundingBoxAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        var ccID = task.Result.Value.ContextCassificationId;

        //var task2 = ContextClassificationService.GetContextClassification(ccID);
        //yield return new WaitUntil(() => task.IsCompleted);

        //if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task.Result.ResponseCode);
        //    yield break;
        //}
        //spriteFromURL.LoadImage(task.Result.Value.ContextCassificationId);
        //currentID = task.Result.Value.Id;

    }
}
