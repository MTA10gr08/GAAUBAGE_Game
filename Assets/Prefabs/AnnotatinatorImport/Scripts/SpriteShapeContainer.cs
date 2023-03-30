using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteShapeController))]
[RequireComponent(typeof(SpriteShapeRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class SpriteShapeContainer : MonoBehaviour {

	public SegmentContainer segmentContainer;
	private SpriteShapeController spriteShapeController = null;
	private SpriteShapeRenderer spriteShapeRenderer = null;
	private PolygonCollider2D polygonCollider2D = null;

	[SerializeField] private SpriteShape SpriteShapeProfile;
	[SerializeField] private string spriteShapeRendererSortingLayer = string.Empty;
	[Range(0f, 1f), SerializeField] private float SegmentTransparency = 0.6f;

	private List<PointBehaviour> points;

	private void Awake()
	{
		polygonCollider2D = GetComponent<PolygonCollider2D>();
		spriteShapeController = GetComponent<SpriteShapeController>();
		spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
	}

	public void SetSpriteShapeVisibility(bool value)
	{
		if(value) {
			var color = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
			color.a = SegmentTransparency;
			spriteShapeRenderer.color = color;
		} else {
			spriteShapeRenderer.color = Color.clear;
		}
		polygonCollider2D.enabled = value;
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
		spriteShapeController.spline.Clear();
		for(int i = 0; i < points.Count; i++) {
			var pos = points[i].transform.position;
			spriteShapeController.spline.InsertPointAt(i, new Vector3(pos.x, pos.y, transform.position.z));
		}
	}

	public void ReColor(Color color)
	{
		color.a = SegmentTransparency;
		spriteShapeRenderer.color = color;
	}

	private void OnValidate()
	{
		//This segment could probably be an extension function
		if(!TryGetComponent(out spriteShapeController)) {
			spriteShapeController = gameObject.AddComponent<SpriteShapeController>();
		}
		spriteShapeController.hideFlags = HideFlags.HideInInspector;

		if(!TryGetComponent(out polygonCollider2D)) {
			polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
		}
		polygonCollider2D.hideFlags = HideFlags.HideInInspector;

		if(!TryGetComponent(out spriteShapeRenderer)) {
			spriteShapeRenderer = gameObject.AddComponent<SpriteShapeRenderer>();
		}
		spriteShapeRenderer.hideFlags = HideFlags.HideInInspector;


		spriteShapeController.spline.Clear();
		spriteShapeController.spriteShape = SpriteShapeProfile;

		polygonCollider2D.pathCount = 0;
		polygonCollider2D.enabled = false;

		spriteShapeRenderer.sortingLayerName = spriteShapeRendererSortingLayer;
		spriteShapeRenderer.color = Color.clear;
	}
}
