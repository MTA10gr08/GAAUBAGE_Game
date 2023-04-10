using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BoxPoint : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    PlayerInput pInput;
    [HideInInspector] public BoxObject boxObject;
    float clickTime = 0f, tapClickThreshold = 0.2f;
    bool CanDestroy = false;
    public GameObject destroyImage;
    public Vector2 Max, Min;
    private void Awake() {
        pInput = new PlayerInput();
        pInput.Enable();
        //btn = GetComponent<Button>();
    }
    private void Start() {
        if (boxObject.BoxPoints[0] == gameObject) {

            destroyImage.SetActive(true);
            CanDestroy = true;
        }
    }
    public void OnPointerClick(PointerEventData eventData) {

    }

    public void OnDrag(PointerEventData eventData) {
        //Vector2 newPos = new Vector2(Mathf.Clamp(pInput.PlayerActions.pos.ReadValue<Vector2>().x, Min.x, Max.x),
        //                             Mathf.Clamp(pInput.PlayerActions.pos.ReadValue<Vector2>().y, Min.y, Max.y));
        Vector2 newPos = pInput.PlayerActions.pos.ReadValue<Vector2>();
        transform.position = newPos;

        boxObject.UpdatePoints();


    }

    public void OnPointerDown(PointerEventData eventData) {
        clickTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData) {
        float clickDuration = Time.time - clickTime;
        if (clickDuration < tapClickThreshold && CanDestroy) // check if the click duration is less than 0.2 seconds
        {
            boxObject.DeleteBox();
        }
    }
    public void SetMinMax(Vector2 min, Vector2 max) {
        Min = min;
        Max = max;
    }
}
