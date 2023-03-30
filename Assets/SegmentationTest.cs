using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentationTest : MonoBehaviour
{
    [SerializeField] PointBehaviour PointPrefab;
    [SerializeField] GameObject outlinePrefab, polygonPrefab;

    Outline outline;
    Polygon polygon;
    public GameObject canvas;

    public List<PointBehaviour> points = new List<PointBehaviour>();
    private void Start() {
        AddPoint(new Vector2(Screen.width / 2 - 50, Screen.height / 2 - 50));
        AddPoint(new Vector2(Screen.width / 2 + 50, Screen.height / 2 - 50));
        AddPoint(new Vector2(Screen.width / 2 + 50, Screen.height / 2 + 50));
        AddPoint(new Vector2(Screen.width / 2 - 50, Screen.height / 2 + 50));
        outline = Instantiate(outlinePrefab, transform).GetComponent<Outline>();
        polygon = Instantiate(polygonPrefab, transform).GetComponent<Polygon>();

        UpdatePositions();
    }

    public void AddPoint(Vector2 pos) {
        InsertPoint(pos, points.Count);
    }

    public void InsertPoint(Vector2 pos, int atIndex) {
        if (points.Count < atIndex) {
            Debug.LogError("Tried to insert point outside bound");
            return;
        }

        //Create the point
        var newPoint = Instantiate(PointPrefab, new Vector3(pos.x, pos.y, PointPrefab.transform.position.z + transform.position.z), Quaternion.identity, canvas.transform);
        //newPoint.segmentContainer = this;
        newPoint.segmentation = this;

        //Update the point list
        points.Insert(atIndex, newPoint);

        //UpdatePoints();
    }
    public void UpdatePositions() {
        outline.UpdateLine(points);
        polygon.UpdatePolygon(points);
    }
}
