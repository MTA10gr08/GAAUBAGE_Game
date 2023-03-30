using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Edgetest : MonoBehaviour, IPointerClickHandler
{
    EdgeCollider2D edgeCollider;
    private void Awake() {
        edgeCollider = GetComponent<EdgeCollider2D>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Outline Pressed");
        var objToSpawn = new GameObject();
        objToSpawn.AddComponent<SpriteRenderer>();
        objToSpawn.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }
}
