using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Point : MonoBehaviour {
	[HideInInspector] public SegmentContainer segmentContainer = null;
	//public Vector2 pos
	//{
	//	get { return transform.position; }
	//}

	//public void Move(Vector2 Pos)
	//{
	//	transform.position = new Vector3(Pos.x, Pos.y, transform.position.z);
	//	segmentContainer.UpdatePoints();
	//}

	//public void RemovePoint()
	//{
	//	segmentContainer.RemovePoint(this);
	//}

	//public void TryFinish()
	//{
	//	if(segmentContainer.points[0] == this) {
	//		segmentContainer.FinishShape();
	//	}
	//}

	//public void SnapLineToPoint()
	//{
	//	if(segmentContainer.IsSegmenting && segmentContainer.points[0] == this) {
	//		//segmentContainer.DrawLineToPoint(transform.position);
	//	}
	//}
}
