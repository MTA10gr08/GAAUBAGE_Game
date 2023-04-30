using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFromURL : MonoBehaviour
{
    [SerializeField] private Vector2 ImagePivot = Vector2.zero;
    private SpriteRenderer spriteRenderer = null;
    //public new BoxCollider2D collider2D = null;
    public GameObject LoadingObject = null;
    public Sprite ErrorImage;

    private string testURL = "https://i.imgur.com/B5FcIac.jpeg";
    //private string testURL = "https://i.imgur.com/tJvxXyv.jpeg";


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector2 WorldPointToImageCord(Vector2 point) {
        return Vector2.zero;
    }


    [UnityEngine.ContextMenu("LoadTestUrl")]
    public void LoadTestUrl() {
        LoadImage(testURL);
    }

    public void ImageUnavailable() {
        spriteRenderer.enabled = true;
        LoadingObject.SetActive(false);
        var texture = ErrorImage.texture;

        var rect = new Rect(0, 0, texture.width, texture.height);
        spriteRenderer.sprite = Sprite.Create(texture, rect, ImagePivot, 1f);
        Camera.main.orthographicSize = (((1 / Camera.main.aspect) * texture.width) / 2);// * 1.1f;
        Camera.main.transform.position = new Vector3(texture.width / 2, -texture.height / 2, Camera.main.transform.position.z);
    }


    public void LoadImage(string URL) {
        StartCoroutine(GetTexture(URL));
        spriteRenderer.enabled = false;

    }
    public void GetImageFromID(Guid imageID) {
        StartCoroutine(GetImage(imageID));
    }
    IEnumerator GetImage(Guid imageID) {
        var task = ImageService.GetImageAsync(imageID);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityEngine.Networking.UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        LoadImage(task.Result.Value.URI);
    }

    IEnumerator GetTexture(string URL) {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(URL)) {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success) {
                Debug.Log(uwr.error);
            } else {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                texture.filterMode = FilterMode.Point;
                var rect = new Rect(0, 0, texture.width, texture.height);
                spriteRenderer.sprite = Sprite.Create(texture, rect, ImagePivot, 1f);
                var width = spriteRenderer.sprite.texture.width;
                var height = spriteRenderer.sprite.texture.height;

                float imageAspect = (float)texture.width / (float)texture.height;//texture.width / texture.height;;//texture.width / texture.height;
                var screenAspect = Camera.main.aspect;
                if (imageAspect < screenAspect) {
                    Camera.main.orthographicSize = (texture.height / 2);// * 1.4f; Values could add a little spacing so images are not against edge but will probably also skew values sligtly?

                } else {
                    Camera.main.orthographicSize = (((1 / screenAspect) * texture.width) / 2);// * 1.1f;
                }

                Camera.main.transform.position = new Vector3(width/2, -height/2, Camera.main.transform.position.z);
                spriteRenderer.enabled = true;
                LoadingObject.SetActive(false);
            }
        }
    }
}