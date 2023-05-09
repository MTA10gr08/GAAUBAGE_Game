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

    Guid currentID = new Guid();

    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        spriteFromURL = GetComponent<SpriteFromURL>();
        StartCoroutine(GetTask());
    }
    private void Start() {

        ctxSelector.contextDropdown.onValueChanged.AddListener(delegate { OnValueChanged(); });
        bgSelector.gameObject.SetActive(true);
        foreach (var item in bgSelector.buttons) {
            item.GetComponent<Button>().onClick.AddListener(delegate { OnValueChanged(); });
        }
        bgSelector.gameObject.SetActive(true);
    }

    IEnumerator GetTask() {
        spriteFromURL.LoadingObject.SetActive(true);
        var task = BackgroundClassificationService.NextBackgroundClassificationAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(task.Result.ResultCode);
            spriteFromURL.ImageUnavailable();
            submitBtn.interactable = false;
            ctxSelector.contextDropdown.interactable = false;
            bgSelector.GetComponent<Button>().interactable = false;
            yield break;
        }
        spriteFromURL.GetImageFromID(task.Result.Value.Image);
        currentID = task.Result.Value.ID;
        Debug.Log(currentID);
    }

    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }

    IEnumerator PostUserValues() {
        spriteFromURL.LoadingObject.SetActive(true);
        BackgroundClassification bgClass = new BackgroundClassification { BackgroundClassificationLabels = bgSelector.CompileStringList() };
        var task = BackgroundClassificationService.PostBackgroundClassificationAsync(bgClass, currentID);
        ContextClassification ctxClass = new ContextClassification { ContextClassificationLabel = ctxSelector.SelectedContext() };
        var task2 = ContextClassificationService.PostContextClassificationAsync(ctxClass, currentID);
        //yield return new WaitUntil(() => task.IsCompleted);
        //if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task.Result.ResponseCode);
        //    Debug.LogError(task.Result.ResultCode);
        //    yield break;
        //}
        yield return new WaitUntil(() => task.IsCompleted && task2.IsCompleted);
        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }

        bgSelector.ClearSelection();
        ctxSelector.ClearSelection();

        StartCoroutine(GetTask());

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

    }
    void OnValueChanged() {
        if (ctxSelector.contextDropdown.value > 0 && bgSelector.CompileStringList().Count > 0) {
            submitBtn.interactable = true;
            return;
        }
        submitBtn.interactable = false;
    }

    public void Skip() {
        StartCoroutine(SkipImageAnnotation());
    }

    IEnumerator SkipImageAnnotation()
    {
        var task = ImageAnnotationService.VoteSkipImageAnnotationAsync(currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            Debug.LogError(task.Result.ResultCode);
            yield break;
        }
        StartCoroutine(GetTask());
    }
}
