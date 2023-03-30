using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;

public class Outline : MonoBehaviour, IPointerDownHandler
{
    //SegmentationTest Segmentation;
    //public List<PointBehaviour> points = new List<PointBehaviour>();
    private LineRenderer lr;
    private EdgeCollider2D edgeCollider;
    private void Awake() {
        lr = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }
    public void UpdateLine(List<PointBehaviour> points) {
        //List<Vector3> pointsAsVector3 = points.Select(x => Camera.main.ScreenToWorldPoint(x.pos)).ToList();
        List<Vector2> pointsAsVector2 = points.Select(x => (Vector2)Camera.main.ScreenToWorldPoint(x.pos)).ToList();

        lr.positionCount = pointsAsVector2.Count();
        lr.SetPositions(pointsAsVector2.Select(x => new Vector3(x.x, x.y, Camera.main.nearClipPlane)).ToArray());

        edgeCollider.points = pointsAsVector2.ToArray();
        edgeCollider.edgeRadius = .1f;

    }
    private void OnMouseDown() {
        
    }
    void OnTouchDown() {

    }
    public void OnPointerClick(PointerEventData eventData) {
        var objToSpawn = new GameObject();
        Debug.Log("Outline Pressed");
        objToSpawn.AddComponent<SpriteRenderer>();
        objToSpawn.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData) {
        throw new System.NotImplementedException();
        Debug.Log("Outline Pressed");
    }
}
