using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;

public class Outline : MonoBehaviour, IPointerClickHandler
{
    public SegmentationTest Segmentation;
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
        pointsAsVector2.Add(pointsAsVector2[0]);
        edgeCollider.points = pointsAsVector2.ToArray();
        edgeCollider.edgeRadius = lr.startWidth;

    }
    public void OnPointerClick(PointerEventData eventData) {
        var objToSpawn = new GameObject();
        Debug.Log("Outline Pressed");
        objToSpawn.AddComponent<SpriteRenderer>();
        objToSpawn.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        ClosestPoint(eventData.position);
    }

    public void ClosestPoint(Vector2 mpos) {
        var points = Segmentation.points;
        (PointBehaviour, float) minPointDistance = (null, Mathf.Infinity), newPointDistance;
        for (int i = 0; i < points.Count(); i++) {
            newPointDistance = (points[i], DistancePointLine(mpos, points[i].pos, points[(i + 1) % points.Count()].pos));
            if (newPointDistance.Item2 < minPointDistance.Item2) {
                minPointDistance = newPointDistance;
            }
        }
        Segmentation.InsertPoint(mpos, points.IndexOf(minPointDistance.Item1) + 1);
    }

    public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd) {
        return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
    }
    public static Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd) {
        Vector3 relativePoint = point - lineStart;
        Vector3 lineDirection = lineEnd - lineStart;
        float length = lineDirection.magnitude;
        Vector3 normalizedLineDirection = lineDirection;
        if (length > .000001f)
            normalizedLineDirection /= length;

        float dot = Vector3.Dot(normalizedLineDirection, relativePoint);
        dot = Mathf.Clamp(dot, 0.0F, length);

        return lineStart + normalizedLineDirection * dot;
    }

}
