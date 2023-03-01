using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxDrawer : MonoBehaviour
{
    LineRenderer ln;
    Vector3[] points = new Vector3[4];
    Vector2 startPos, endPos;

    public GameObject BoundingBoxeObject;
    private GameObject CurrentBoundingBoxObject;
    public List<GameObject> BoundingBoxes = new List<GameObject>();
    bool BoxMode = false;

    private void Start() {
    }

    public void StartBoxMode() {
        Debug.Log("BoxStarted");
        BoxMode = true;
    }

    public void MakeBox(InputAction.CallbackContext ctx) {
        if (BoxMode != true) {
            return;
        }
        Debug.Log(ctx.phase);
        switch (ctx.phase) {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                CurrentBoundingBoxObject = Instantiate(BoundingBoxeObject);
                BoundingBoxes.Add(CurrentBoundingBoxObject);
                ln = CurrentBoundingBoxObject.GetComponent<LineRenderer>();
                startPos = GetCurrentMouseWorldPosition();
                break;
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Canceled:
                ln = null;
                BoxMode = false;
                endPos = GetCurrentMouseWorldPosition();
                break;
            default:
                break;
        }
    }

    public static Vector3 GetCurrentMouseWorldPosition() {
        var screenPos = Mouse.current.position.ReadValue();
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
        return worldPos;
    }
    private void LateUpdate() {
        var cursor = GetCurrentMouseWorldPosition();
        if (ln != null) {
            points[0] = new Vector3(startPos.x, startPos.y, cursor.z);
            points[1] = new Vector3(startPos.x, cursor.y, cursor.z);
            points[2] = new Vector3(cursor.x, cursor.y, cursor.z);
            points[3] = new Vector3(cursor.x, startPos.y, cursor.z);
            ln.SetPositions(points);
        }
    }
}
