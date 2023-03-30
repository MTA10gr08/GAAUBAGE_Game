using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class OutlineContainer : MonoBehaviour {

	public SegmentContainer segmentContainer;
	private LineRenderer lineRenderer;
	private EdgeCollider2D edgeCollider2D;

	[SerializeField] private float LineDefaultScale = 1, onHoverScale = 2;
	[SerializeField] private Material LineMaterial = null;
	[SerializeField] private Color LineColor = Color.white;
	[SerializeField] private string LineSortingLayer = string.Empty;
	private float Scale = 0;

	private List<PointBehaviour> points = new List<PointBehaviour>();
	private OutlineBehavior outlineBehavior = OutlineBehavior.DrawToCursor;

	private void Awake()
	{
		Scale = LineDefaultScale;
		lineRenderer = GetComponent<LineRenderer>();
		edgeCollider2D = GetComponent<EdgeCollider2D>();

		var size = (Vector3.one * (Camera.main.orthographicSize * 2 * Scale)).x;
		lineRenderer.startWidth = size;
		edgeCollider2D.edgeRadius = size / 2;
	}

	private void Update()
	{
		var size = (Vector3.one * (Camera.main.orthographicSize * 2 * Scale)).x;
		lineRenderer.startWidth = size;
		edgeCollider2D.edgeRadius = size / 2;

		if(outlineBehavior == OutlineBehavior.DrawToCursor) {
			var pos = Camera.main.ScreenToWorldPoint(Vector2.zero); //Should be mouse/touch pos
			lineRenderer.positionCount = points.Count() + 1;
			lineRenderer.SetPosition(points.Count(), new Vector3(pos.x, pos.y, 0));
		}
	}

	public void ClosestPoint(Vector2 mpos)
	{
		(PointBehaviour, float) minPointDistance = (null, Mathf.Infinity), newPointDistance;
		for(int i = 0; i < points.Count(); i++) {
			newPointDistance = (points[i], DistancePointLine(mpos, points[i].pos, points[(i + 1) % points.Count()].pos));
			if(newPointDistance.Item2 < minPointDistance.Item2) {
				minPointDistance = newPointDistance;
			}
		}
		segmentContainer.InsertPoint(mpos, points.IndexOf(minPointDistance.Item1) + 1);
	}

	public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
	}

	public static Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		Vector3 relativePoint = point - lineStart;
		Vector3 lineDirection = lineEnd - lineStart;
		float length = lineDirection.magnitude;
		Vector3 normalizedLineDirection = lineDirection;
		if(length > .000001f)
			normalizedLineDirection /= length;

		float dot = Vector3.Dot(normalizedLineDirection, relativePoint);
		dot = Mathf.Clamp(dot, 0.0F, length);

		return lineStart + normalizedLineDirection * dot;
	}

	public void makebig()
    {
		Scale = onHoverScale;
	}
	public void makesmol()
	{
		Scale = LineDefaultScale;
	}


	public void SetOutlineBehavior(OutlineBehavior Value)
	{
		outlineBehavior = Value;
		lineRenderer.loop = outlineBehavior == OutlineBehavior.Loop;
		UpdatePoints();
	}

	public void SetPoints(List<PointBehaviour> points)
	{
		this.points = points;
		UpdatePoints();
	}

	public void AddPoint(PointBehaviour point)
	{
		points.Add(point);
		UpdatePoints();
	}

	public void RemovePoint(PointBehaviour point)
	{
		points.Remove(point);
		UpdatePoints();
	}

	[UnityEngine.ContextMenu("UpdatePoints")]
	private void UpdatePoints()
	{
		List<Vector2> pointsAsVector2 = points.Select(x => x.pos).ToList();

		lineRenderer.positionCount = pointsAsVector2.Count();
		lineRenderer.SetPositions(pointsAsVector2.Select(x => (Vector3)x).ToArray());

		if(outlineBehavior == OutlineBehavior.Loop) {
			pointsAsVector2.Add(pointsAsVector2[0]);
		}
		edgeCollider2D.points = pointsAsVector2.ToArray();

	}

	private void OnValidate()
	{
		//This segment could probably be an extension function
		if(!TryGetComponent(out lineRenderer)) {
			lineRenderer = gameObject.AddComponent<LineRenderer>();
		}
		lineRenderer.hideFlags = HideFlags.HideInInspector;

		if(!TryGetComponent(out edgeCollider2D)) {
			edgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
		}
		edgeCollider2D.hideFlags = HideFlags.HideInInspector;

		lineRenderer.positionCount = 0;
		lineRenderer.useWorldSpace = false;
		lineRenderer.material = LineMaterial;
		lineRenderer.startColor = LineColor;
		lineRenderer.sortingLayerName = LineSortingLayer;
		lineRenderer.numCapVertices = 1;
		lineRenderer.numCornerVertices = 1;

		edgeCollider2D.SetPoints(new List<Vector2>());

		UpdatePoints();
	}

	public enum OutlineBehavior {
		None,
		DrawToCursor,
		Loop
	}
}
