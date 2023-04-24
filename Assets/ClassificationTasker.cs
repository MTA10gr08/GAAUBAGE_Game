using GAAUBAGE_Game.API.Services;
using GAAUBAGE_Game.API.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        var task = TrashSuperCategoryService.NextTrashSuperCategoryAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        //currentID = task.Result.Value.ImageID;
        //spriteFromURL.GetImageFromID(task.Result.Value.ImageID);

    }
    public void submitValuesToServer() {
        StartCoroutine(PostUserValues());
    }
    IEnumerator PostUserValues() {
        Debug.Log("You sure did post those values"); //Skal jeg efterlade ImageAnnotation tom? eller hvad skal i den?
        TrashSuperCategory sCategory = new TrashSuperCategory { SuperCategory = categoryPopulator.superCategoryDropdown.options[categoryPopulator.superCategoryDropdown.value].text };
        Debug.Log(sCategory.SuperCategory);
        //var task = TrashSuperCategoryService.PostTrashSuperCategoryAsync(sCategory, currentID);
        //yield return new WaitUntil(() => task.IsCompleted);
        //if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task.Result.ResponseCode);
        //    yield break;
        //}

        TrashSubCategory category = new TrashSubCategory { Category = categoryPopulator.categoryDropdown.options[categoryPopulator.categoryDropdown.value].text };
        Debug.Log(category.Category);
        //var task2 = TrashCategoryService.PostTrashCategoryAsync(category, currentID);
        //yield return new WaitUntil(() => task2.IsCompleted);
        //if (task2.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
        //    Debug.LogError(task2.Result.ResponseCode);
        //    yield break;
        //}
        yield return null;
        StartCoroutine(GetTask());
    }
}