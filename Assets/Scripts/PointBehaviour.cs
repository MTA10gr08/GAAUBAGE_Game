using UnityEngine;
using UnityEngine.EventSystems;

public class PointBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler//  IPointerClickHandler
{
    [HideInInspector]
    public Segmentation segmentation;

    PlayerInput pInput;
    //float clickTime = 0f, doubleClickThreshhold = 0.5f;
    float clickTime = 0f, tapClickThreshold = 0.2f;

    private void Awake() {
        pInput = new PlayerInput();
        pInput.Enable();
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Pointer Click");
        //if (Time.time - clickTime < doubleClickThreshhold) {
        //    if (segmentation.points.Count < 4) {
        //        return;
        //    }
        //    segmentation.RemovePoint(this);
        //}
        //clickTime = Time.time;
    }


    public void OnPointerDown(PointerEventData eventData) {
        clickTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData) {
        float clickDuration = Time.time - clickTime;
        if (clickDuration < tapClickThreshold) // check if the click duration is less than 0.2 seconds
        {
            if (segmentation.points.Count < 4) {
                return;
            }
            segmentation.RemovePoint(this);
        }
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
