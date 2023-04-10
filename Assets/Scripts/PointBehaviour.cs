using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PointBehaviour : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public SegmentationTest segmentation;

    PlayerInput pInput;
    Button btn;
    bool Dragging = false;
    float clickTime = 0f, doubleClickThreshhold = 0.5f;

    private void Awake() {
        pInput = new PlayerInput();
        pInput.Enable();
        btn = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (Time.time - clickTime < doubleClickThreshhold) {
            Debug.Log("DoubleClicked");
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
        Debug.Log("WERE YOU FKN DRAGGING");
        transform.position = pInput.PlayerActions.pos.ReadValue<Vector2>();
        Debug.Log(pInput.PlayerActions.pos.ReadValue<Vector2>());

        //segmentContainer?.UpdatePoints();
        segmentation?.UpdatePositions();

    }

    public void OnEndDrag(PointerEventData eventData) {

    }

    public Vector2 pos {
        get { return transform.position; }
    }
}
