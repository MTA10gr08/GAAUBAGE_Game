using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAndContextTasker : MonoBehaviour
{
    SpriteFromURL spriteFromURL;

    public BackgroundContextSelector bgSelector;
    public ContextSelector ctxSelector;

    public Button submitBtn;

    Guid currentID;

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        spriteFromURL = GetComponent<SpriteFromURL>();
        StartCoroutine(GetTask());
    }
    private void Start() {
        ctxSelector.contextDropdown.onValueChanged.AddListener(delegate { OnValueChanged(); });
        foreach (var item in bgSelector.buttons) {

            item.button.onClick.AddListener(delegate { OnValueChanged(); });
        }
    }

    IEnumerator GetTask() {
        var task = BackgroundClassificationService.NextBackgroundClassificationAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        spriteFromURL.GetImageFromID(task.Result.Value.Image);
        currentID = task.Result.Value.ID;
    }

    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }

    IEnumerator PostUserValues() {
        Debug.Log("You sure did post those values");
        BackgroundClassification bgClass = new BackgroundClassification { BackgroundClassificationLabels = bgSelector.CompileStringList() };
        var task = BackgroundClassificationService.PostBackgroundClassificationAsync(bgClass, currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        ContextClassification ctxClass = new ContextClassification { Category = ctxSelector.SelectedContext() };
        var task2 = ContextClassificationService.PostContextClassificationAsync(ctxClass, currentID);
        yield return new WaitUntil(() => task2.IsCompleted);
        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }
        yield return null;
        StartCoroutine(GetTask());
    }
    void OnValueChanged() {

        if (ctxSelector.contextDropdown.value > 0 && bgSelector.CompileStringList().Count > 0) {
            submitBtn.interactable = true;
            return;
        }
        submitBtn.interactable = false;
    }
}
