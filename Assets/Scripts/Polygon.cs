using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;

public class Polygon : MonoBehaviour
{
	private SpriteShapeController spriteShapeController = null;
	private SpriteShapeRenderer spriteShapeRenderer = null;
	private PolygonCollider2D polygonCollider2D = null;

	[SerializeField] private SpriteShape SpriteShapeProfile;
	[SerializeField] private string spriteShapeRendererSortingLayer = string.Empty;
	[Range(0f, 1f), SerializeField] private float SegmentTransparency = 0.6f;

	private void Awake() {
		polygonCollider2D = GetComponent<PolygonCollider2D>();
		spriteShapeController = GetComponent<SpriteShapeController>();
		spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
	}
	public void UpdatePolygon(List<PointBehaviour> points) {
		var pointsAsVector2 = points.Select(x => Camera.main.ScreenToWorldPoint(x.pos)).ToList();
		//var pointsAsVector2 = points.Select(x => x.pos).ToList();

		//lr.SetPositions(pointsAsVector2.Select(x => new Vector3(x.x, x.y, Camera.main.nearClipPlane)).ToArray());

		//if (outlineBehavior == OutlineBehavior.Loop) {
		//    pointsAsVector2.Add(pointsAsVector2[0]);
		//}
		//edgeCollider2D.points = pointsAsVector2.ToArray();

		spriteShapeController?.spline.Clear();
		for (int i = 0; i < pointsAsVector2.Count; i++) {
			var pos = points[i].transform.position;
			spriteShapeController.spline.InsertPointAt(i, pointsAsVector2[i]);
		}

	}
	private void OnValidate() {
		//This segment could probably be an extension function
		if (!TryGetComponent(out spriteShapeController)) {
			spriteShapeController = gameObject.AddComponent<SpriteShapeController>();
		}
		spriteShapeController.hideFlags = HideFlags.HideInInspector;

		if (!TryGetComponent(out polygonCollider2D)) {
			polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
		}
		polygonCollider2D.hideFlags = HideFlags.HideInInspector;

		if (!TryGetComponent(out spriteShapeRenderer)) {
			spriteShapeRenderer = gameObject.AddComponent<SpriteShapeRenderer>();
		}
		spriteShapeRenderer.hideFlags = HideFlags.HideInInspector;


		spriteShapeController.spline.Clear();
		spriteShapeController.spriteShape = SpriteShapeProfile;

		polygonCollider2D.pathCount = 0;
		polygonCollider2D.enabled = false;

		spriteShapeRenderer.sortingLayerName = spriteShapeRendererSortingLayer;
		var color = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
		color.a = SegmentTransparency;
		spriteShapeRenderer.color = color;
	}
}
