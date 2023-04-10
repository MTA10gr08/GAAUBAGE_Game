using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;
using TMPro;
using System;

[DisallowMultipleComponent]
public class SegmentContainer : MonoBehaviour
{

    [HideInInspector] public ImageSegmenter imageSegmenter = null;
    [HideInInspector] public List<PointBehaviour> points = new List<PointBehaviour>();
    [HideInInspector] public ImageCatagoryScriptableObject imageCatagory = null;
    public DateTime Created, Finished, LastEdit;
    public GameObject canvas;

    public bool IsSegmenting { get; private set; }

    [SerializeField] private SpriteShapeContainer spriteShapeContainer = null;
    [SerializeField] private OutlineContainer outlineContainer = null;
    [SerializeField] public TextMeshPro displayText = null;
    [SerializeField] private PointBehaviour PointPrefab = null;

    private void Awake()
    {
        points = new List<PointBehaviour>();
        IsSegmenting = true;
    }

    private void Start()
    {
        outlineContainer.segmentContainer = this;
        spriteShapeContainer.segmentContainer = this;

        if (displayText)
            displayText.enabled = false;

        Created = DateTime.UtcNow;

        AddPoint(new Vector2(Screen.width/2-50, Screen.height/2-50));
        AddPoint(new Vector2(Screen.width/2+50, Screen.height/2-50));
        AddPoint(new Vector2(Screen.width/2+50, Screen.height/2+50));
        AddPoint(new Vector2(Screen.width/2-50, Screen.height/2+50));

    }

    public void AssignCatagory(ImageCatagoryScriptableObject garbageCategory)
    {
        imageCatagory = garbageCategory;
        spriteShapeContainer.ReColor(garbageCategory.Color);
        displayText.text = garbageCategory.Name;
        imageSegmenter.EnableSubmission();
    }

    #region Point List modification methods
    public void AddPoint(Vector2 pos)
    {
        InsertPoint(pos, points.Count);
    }

    public void InsertPoint(Vector2 pos, int atIndex)
    {
        if (points.Count < atIndex)
        {
            Debug.LogError("Tried to insert point outside bound");
            return;
        }

        //Create the point
        var newPoint = Instantiate(PointPrefab, new Vector3(pos.x, pos.y, PointPrefab.transform.position.z + transform.position.z), Quaternion.identity, canvas.transform);
        //newPoint.segmentContainer = this;

        //Update the point list
        points.Insert(atIndex, newPoint);

        UpdatePoints();
    }

    public void RemovePoint(PointBehaviour point)
    {
        points.Remove(point);
        Destroy(point.gameObject);

        UpdatePoints();
    }
    #endregion

    public void UpdatePoints()
    {
        outlineContainer.SetPoints(points);
        spriteShapeContainer.SetPoints(points);
        LastEdit = DateTime.UtcNow;
        ValidateShape();
    }

    [UnityEngine.ContextMenu("ValidateShape")]
    public void ValidateShape()
    {

        if (isShapeComplex(points.Select(x => x.pos).ToArray()))
        {
            RemovePoint(points.Last());
            Debug.LogWarning("Shape is complex");
        }

        if ((!IsSegmenting && points.Count() < 3) || points.Count() <= 0)
        {
            Destroy(gameObject);
        }

        if (displayText)
        {
            displayText.transform.position = centroid(points.Select(x => x.pos).ToList());
        }
    }

    #region Shape Complexity calculation
    private bool isShapeComplex(Vector2[] points)
    {
        for (int j = 2; j < points.Count() - 1; j++)
        {
            if (intersect(points[0], points[1], points[j], points[j + 1]))
            {
                return true;
            }
        }

        for (int i = 1; i < points.Count(); i++)
        {
            for (int j = i + 2; j < points.Count() - 1; j++)
            {
                if (intersect(points[i], points[i + 1], points[j], points[j + 1]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private double ccw(Vector2 I, Vector2 J, Vector2 K)
    {
        double ccw = (K.y - I.y) * (J.x - I.x) - (J.y - I.y) * (K.x - I.x);
        return ccw > 0.0d ? 1d : ccw < 0.0d ? -1d : 0d;
    }

    private bool intersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        return ccw(A, C, D) != ccw(B, C, D) && ccw(A, B, C) != ccw(A, B, D);
    }
    #endregion

    //Look at this
    public void FinishShape()
    {
        IsSegmenting = false;
        ValidateShape();

        //Remove line to the mouse and make the linerendere loop
        outlineContainer.SetOutlineBehavior(OutlineContainer.OutlineBehavior.Loop);


        spriteShapeContainer.SetSpriteShapeVisibility(true);

        //Make the text visible
        displayText.enabled = true;

        //It is done
        Finished = DateTime.UtcNow;
        imageSegmenter.FinishSegment();
    }

    public Vector2 centroid(List<Vector2> points)
    {
        Vector2 centroid = Vector2.zero;

        if(points.Count != 0)
        {
            for (int i = 0; i < points.Count; i++)
            {
                centroid += points[i];
            }
            centroid = centroid / (points.Count);
        }

        return centroid;
    }


}