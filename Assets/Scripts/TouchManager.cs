using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    PlayerInput test;
    private InputAction touchPositionAction, touchPressAction;
    public GameObject Test, canvas;
    private GameObject currentObject;
    private bool testa;


    private void Awake() {
        //playerInput = GetComponent<PlayerInput>();
        //touchPositionAction = playerInput.actions["pos"];
        //touchPressAction = playerInput.actions["Interact"];
        //touchPressAction.performed += delegate { Instantiate(Test, ScreenToWorld(touchPositionAction.ReadValue<Vector2>()), Quaternion.identity ); };

        test = new PlayerInput();
        test.Enable();
        test.PlayerActions.Testa.performed += (ctx) => {
            testa = ctx.ReadValueAsButton();
            
        };
    }
    private void Update() {

        switch (test.PlayerActions.Testa.phase) {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                currentObject = null;
                break;
            case InputActionPhase.Started:

                break;
            case InputActionPhase.Performed:
                if (currentObject == null) {
                    currentObject = Instantiate(Test, canvas.transform);
                    currentObject.GetComponent<Button>().onClick.AddListener(delegate { Debug.Log(currentObject.name); });
                    currentObject.GetComponent<Button>().onClick.AddListener( delegate { setCurrentObject(currentObject); });

                }
                currentObject.transform.position = test.PlayerActions.pos.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }
        //switch (touchPressAction.phase) {
        //    case InputActionPhase.Disabled:
        //        break;
        //    case InputActionPhase.Waiting:
        //        break;
        //    case InputActionPhase.Started:
        //        if (currentObject == null) {
        //            currentObject = Instantiate(Test);
        //        }

        //        break;
        //    case InputActionPhase.Performed:
        //        currentObject = null;
        //        break;
        //    case InputActionPhase.Canceled:
        //        break;
        //    default:
        //        break;
        //}
        //if (currentObject!=null) {
        //    currentObject.transform.position = ScreenToWorld(touchPositionAction.ReadValue<Vector2>());
        //}
    }
    Vector3 ScreenToWorld(Vector2 pos) {
        return Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
    }
    public void setCurrentObject(GameObject obj) {
        currentObject = obj;
    }
}
