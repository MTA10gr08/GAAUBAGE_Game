using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SpriteFromURLSegmentation : MonoBehaviour
{
	[SerializeField] private Vector2 ImagePivot = Vector2.zero;
	private SpriteRenderer spriteRenderer = null;
	//public new BoxCollider2D collider2D = null;
	//public GameObject LoadingObject = null;

	//private string testURL = "https://i.imgur.com/B5FcIac.jpeg";
	private string testURL = "https://i.imgur.com/tJvxXyv.jpeg";
	public ImageMask mask;
	public Segmentation segmentation;


	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		LoadTestUrl();
	}

	public Vector2 WorldPointToImageCord(Vector2 point) {
		return Vector2.zero;
	}


	[UnityEngine.ContextMenu("LoadTestUrl")]
	public void LoadTestUrl() {
		LoadImage(testURL);
	}

	public void LoadImage(string URL) {
		StartCoroutine(GetTexture(URL));
		//LoadingObject.SetActive(true);
		spriteRenderer.enabled = false;
		var pos = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3(0, 0, pos.z);
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
				
				//My wacky attempt at centering the camera on the subimage/box provided by the server
				Vector2 p1 = new Vector2(0, 1000), p2 = new Vector2(1000, 0);
				Vector2[] vList = { p1, p2};

				var width = Mathf.Abs(p1.x - p2.x);//spriteRenderer.sprite.texture.width;
				var height = Mathf.Abs(p1.y - p2.y);//spriteRenderer.sprite.texture.height;
				var center = CenterOfVectors(vList);
				var pos = Camera.main.transform.position;
				Camera.main.transform.position = new Vector3(center.x, -center.y, pos.z);
				//collider2D.size = new Vector2(width, height);
				if (width * 2 < height) {
					Camera.main.orthographicSize = (height / 2) + (height / 5);
				} else {
					Camera.main.orthographicSize = width + (width / 10);
				}
				mask.updateImageMask(width, height);
				segmentation.UpdatePositions();
				spriteRenderer.enabled = true;
				//LoadingObject.SetActive(false);
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
}
