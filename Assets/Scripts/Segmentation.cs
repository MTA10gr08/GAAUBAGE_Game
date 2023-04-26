using GAAUBAGE_Game.API.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GAAUBAGE_Game.API.Models;

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
    public MultiPolygon CompileMultiPolygon() {
        var Coordinates = points.ConvertAll(x => new Coordinate() { X = x.transform.position.x, Y = x.transform.position.y });
        Coordinates.Add(Coordinates.First());
        MultiPolygon mp = new MultiPolygon() {
            Polygons = new List<GAAUBAGE_Game.API.Models.Polygon>()
            {
                new GAAUBAGE_Game.API.Models.Polygon()
                {
                    Shell = new LinearRing { Coordinates = Coordinates },
                    Holes = new()

                }
            }
        };
        return mp;
    }

    public void ClearPolygon() {
        Debug.Log("Resetting Polygon");
        Destroy(polygon.gameObject);

        foreach (var point in points) {
            Destroy(point.gameObject, .2f);
        }
        points.Clear();


        polygon = Instantiate(polygonPrefab, transform).GetComponent<Polygon>();
        AddPoint(new Vector2(Screen.width / 2 - 50, Screen.height / 2 - 50));
        AddPoint(new Vector2(Screen.width / 2 + 50, Screen.height / 2 - 50));
        AddPoint(new Vector2(Screen.width / 2 + 50, Screen.height / 2 + 50));
        AddPoint(new Vector2(Screen.width / 2 - 50, Screen.height / 2 + 50));
    }

}
