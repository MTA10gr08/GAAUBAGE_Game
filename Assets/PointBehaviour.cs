using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PointBehaviour : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public SegmentContainer segmentContainer;
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

    private void Update() {

        if (btn.spriteState.pressedSprite) {
            transform.position = pInput.PlayerActions.pos.ReadValue<Vector2>();
        }
        switch (btn.spriteState) {
            default:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log(Time.time - clickTime);
        if (Time.time - clickTime < doubleClickThreshhold) {
            Debug.Log("DoubleClicked");
                segmentContainer?.RemovePoint(this);
        }
        clickTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData eventData) {
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("WERE YOU FKN DRAGGING");
        transform.position = pInput.PlayerActions.pos.ReadValue<Vector2>();
        Debug.Log(pInput.PlayerActions.pos.ReadValue<Vector2>());
        
        segmentContainer?.UpdatePoints();
        segmentation?.UpdatePositions();

    }

    public void OnEndDrag(PointerEventData eventData) {

    }

    public Vector2 pos {
        get { return transform.position; }
    }
}
