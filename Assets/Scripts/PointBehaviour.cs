using UnityEngine;
using UnityEngine.EventSystems;

public class PointBehaviour : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Segmentation segmentation;

    PlayerInput pInput;
    float clickTime = 0f, doubleClickThreshhold = 0.5f;

    private void Awake() {
        pInput = new PlayerInput();
        pInput.Enable();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (Time.time - clickTime < doubleClickThreshhold) {
            if (segmentation.points.Count < 4) {
                return;
            }
            segmentation.RemovePoint(this);
        }
        clickTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData eventData) {
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = pInput.PlayerActions.pos.ReadValue<Vector2>();

        //segmentContainer?.UpdatePoints();
        segmentation?.UpdatePositions();

    }

    public void OnEndDrag(PointerEventData eventData) {

    }

    public Vector2 pos {
        get { return transform.position; }
    }
}
