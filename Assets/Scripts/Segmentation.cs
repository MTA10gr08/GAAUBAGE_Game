using System.Collections.Generic;
using UnityEngine;

public class Segmentation : MonoBehaviour
{
    [SerializeField] PointBehaviour PointPrefab;
    [SerializeField] GameObject outlinePrefab, polygonPrefab;

    Outline outline;
    Polygon polygon;
    public GameObject canvas;

    public List<PointBehaviour> points = new List<PointBehaviour>();

    private void Awake() {
        outline = Instantiate(outlinePrefab, transform).GetComponent<Outline>();
        outline.Segmentation = this;
        polygon = Instantiate(polygonPrefab, transform).GetComponent<Polygon>();
        AddPoint(new Vector2(Screen.width / 2 - 50, Screen.height / 2 - 50));
        AddPoint(new Vector2(Screen.width / 2 + 50, Screen.height / 2 - 50));
        AddPoint(new Vector2(Screen.width / 2 + 50, Screen.height / 2 + 50));
        AddPoint(new Vector2(Screen.width / 2 - 50, Screen.height / 2 + 50));
    }

    private void Start() {
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
        newPoint.segmentation = this;

        //Update the point list
        points.Insert(atIndex, newPoint);
        UpdatePositions();
    }
    public void UpdatePositions() {
        outline.UpdateLine(points);
        polygon.UpdatePolygon(points);
    }
    public void RemovePoint(PointBehaviour point) {
        points.Remove(point);
        Destroy(point.gameObject);

        UpdatePositions();
    }
    public GAAUBAGE_Game.API.Models.MultiPolygon CompileMultiPolygon() {
        GAAUBAGE_Game.API.Models.MultiPolygon mp = new GAAUBAGE_Game.API.Models.MultiPolygon();

        GAAUBAGE_Game.API.Models.Polygon polygon = new GAAUBAGE_Game.API.Models.Polygon();

        GAAUBAGE_Game.API.Models.LinearRing ring = new GAAUBAGE_Game.API.Models.LinearRing();

        foreach (var point in points) {
            GAAUBAGE_Game.API.Models.Coordinate coord = new GAAUBAGE_Game.API.Models.Coordinate();

            coord.Longitude = point.transform.position.x;
            coord.Latitude = point.transform.position.y;
            ring.Coordinates.Add(coord);
        }
        polygon.Shell = ring;
        mp.Polygons.Add(polygon);

        return mp;
    }

}
