using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFromURL : MonoBehaviour {
	[SerializeField] private Vector2 ImagePivot = Vector2.zero;
	private SpriteRenderer spriteRenderer = null;
	public new BoxCollider2D collider2D = null;
	public GameObject LoadingObject = null;

	private string testURL = "https://i.imgur.com/B5FcIac.jpeg";

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public Vector2 WorldPointToImageCord(Vector2 point)
	{
		return Vector2.zero;
	}


	[UnityEngine.ContextMenu("LoadTestUrl")]
	public void LoadTestUrl()
	{
		LoadImage(testURL);
	}

	public void LoadImage(string URL)
	{
		StartCoroutine(GetTexture(URL));
		LoadingObject.SetActive(true);
		spriteRenderer.enabled = false;
		var pos = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3(0, 0, pos.z);
	}

	IEnumerator GetTexture(string URL)
	{
		using(UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(URL)) {
			yield return uwr.SendWebRequest();

			if(uwr.result != UnityWebRequest.Result.Success) {
				Debug.Log(uwr.error);
			} else {
				Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
				texture.filterMode = FilterMode.Point;
				var rect = new Rect(0, 0, texture.width, texture.height);
				spriteRenderer.sprite = Sprite.Create(texture, rect, ImagePivot, 1f);
				var width = spriteRenderer.sprite.texture.width;
				var height = spriteRenderer.sprite.texture.height;
				collider2D.size = new Vector2(width, height);
				Camera.main.orthographicSize = height / 2;
				spriteRenderer.enabled = true;
				LoadingObject.SetActive(false);
			}
		}
	}
}
