using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SpriteFromURLSegmentation : MonoBehaviour
{
    [SerializeField] private Vector2 ImagePivot = Vector2.one;
    private SpriteRenderer spriteRenderer = null;
    //public new BoxCollider2D collider2D = null;
    public GameObject LoadingObject = null;

    private string testURL = "https://i.imgur.com/B5FcIac.jpeg";
    //private string testURL = "https://i.imgur.com/tJvxXyv.jpeg";
    public ImageMask mask;
    public Segmentation segmentation;
    private SubImageAnnotation subAnnotation = new SubImageAnnotation();
    public Sprite ErrorImage;

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
        //mask.enabled = false;
        spriteRenderer.enabled = true;
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        LoadingObject.SetActive(false);
        var texture = ErrorImage.texture;

        var rect = new Rect(0, 0, texture.width, texture.height);
        spriteRenderer.sprite = Sprite.Create(texture, rect, ImagePivot, 1f);
        Camera.main.orthographicSize = (((1 / Camera.main.aspect) * texture.width) / 2);// * 1.1f;
        Camera.main.transform.position = new Vector3(texture.width / 2, -texture.height / 2, Camera.main.transform.position.z);
    }

    public void GetImageFromID(Guid imageID) {
        StartCoroutine(GetImage(imageID));
    }
    public void GetImageFromTask(SubImageAnnotation annotation) {
        subAnnotation = annotation;
        StartCoroutine(GetImage(annotation.Image));
    }

    IEnumerator GetImage(Guid imageID) {
        var task = ImageService.GetImageAsync(imageID);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result.ResultCode != UnityWebRequest.Result.Success) {
            Debug.LogError(task.Result.ResponseCode);
            yield break;
        }
        LoadImage(task.Result.Value.URI);
    }

    public void LoadImage(string URL) {
        StartCoroutine(GetTexture(URL));
        spriteRenderer.enabled = false;

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

                var pos = Camera.main.transform.position;


                Camera.main.transform.position = new Vector3(subAnnotation.X + (subAnnotation.Width / 2), (-subAnnotation.Y + (subAnnotation.Height / 2)) - subAnnotation.Height, pos.z);
                float imageAspect = (float)subAnnotation.Width / (float)subAnnotation.Height;//texture.width / texture.height;;//texture.width / texture.height;
                var screenAspect = Camera.main.aspect;

                if (imageAspect < screenAspect) {
                    Camera.main.orthographicSize = (subAnnotation.Height / 2);

                } else {
                    Camera.main.orthographicSize = ((1 / screenAspect) * subAnnotation.Width)/2;
                }
                mask.updateImageMask(subAnnotation.Width, subAnnotation.Height);
                segmentation?.UpdatePositions();
                spriteRenderer.enabled = true;
                LoadingObject.SetActive(false);
                subAnnotation = null; //questionable
            }
        }
    }

    public Vector2 CenterOfVectors(Vector2[] vectors) {
        Vector2 sum = Vector2.zero;
        if (vectors == null || vectors.Length == 0) {
            return sum;
        }

        foreach (Vector2 vec in vectors) {
            sum += vec;
        }
        return sum / vectors.Length;
    }

    //private void OnDrawGizmos() {
    //    var center = new Vector3(subAnnotation.X + (subAnnotation.Width / 2), (-subAnnotation.Y + (subAnnotation.Height / 2) - subAnnotation.Height), 0);
    //    var size = new Vector3(subAnnotation.Width, subAnnotation.Height, 0);
    //    Gizmos.DrawWireCube(center, size);
    //}
}
