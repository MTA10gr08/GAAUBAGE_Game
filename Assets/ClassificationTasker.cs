using GAAUBAGE_Game.API.Services;
using GAAUBAGE_Game.API.Models;
using System.Collections;
using UnityEngine;
using System;
using GAAUBAGE_Game.API.Networking;

public class ClassificationTasker : MonoBehaviour
{
    SpriteFromURLSegmentation spriteFromURL;
    public CategoryPopulator categoryPopulator;
    Guid currentID;


    private void Awake() {
        APIRequestHandler.JWT = PlayerPrefs.GetString("JWT");
        spriteFromURL = GetComponent<SpriteFromURLSegmentation>();
        StartCoroutine(GetTask());
    }

    IEnumerator GetTask() {
        spriteFromURL.LoadingObject.SetActive(true);
        var task = TrashSuperCategoryService.NextTrashSuperCategoryAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        currentID = task.Result.Value.ID;
        Debug.Log(task.Result.Value.Image.ToString());
        spriteFromURL.GetImageFromTask(task.Result.Value);
    }
    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }
    IEnumerator PostUserValues() {
        TrashSuperCategory sCategory = new TrashSuperCategory { TrashSuperCategoryLabel = categoryPopulator.superCategoryDropdown.options[categoryPopulator.superCategoryDropdown.value].text };
        Debug.Log(sCategory.TrashSuperCategoryLabel);
        var task = TrashSuperCategoryService.PostTrashSuperCategoryAsync(sCategory, currentID);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }

        TrashSubCategory category = new TrashSubCategory { TrashSubCategoryLabel = categoryPopulator.categoryDropdown.options[categoryPopulator.categoryDropdown.value].text };
        Debug.Log(category.TrashSubCategoryLabel);
        var task2 = TrashSubCategoryService.PostTrashSubCategoryAsync(category, currentID);
        yield return new WaitUntil(() => task2.IsCompleted);
        if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task2.Result.ResponseCode);
            yield break;
        }
        yield return null;
        StartCoroutine(GetTask());
    }
}
