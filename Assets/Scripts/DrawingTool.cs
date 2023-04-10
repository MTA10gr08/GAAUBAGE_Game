using UnityEngine;
using UnityEngine.InputSystem;
public class Draw : MonoBehaviour
{
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector3 lastPos;


    public void Drawing(InputAction.CallbackContext ctx) {
        Debug.Log("Draw");
        //Debug.Log(ctx.phase);
        switch (ctx.phase) {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                GameObject brushInstance = Instantiate(brush);
                currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

                //because you gotta have 2 points to start a line renderer, 
                Vector2 mousePos = BoxDrawer.GetCurrentMouseWorldPosition();

                currentLineRenderer.SetPosition(0, mousePos);
                currentLineRenderer.SetPosition(1, mousePos);

                break;
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Canceled:
                currentLineRenderer = null;
                break;
            default:
                break;
        }
    }

    private void LateUpdate() {
        Debug.Log("LateUpdate");
        if (currentLineRenderer != null) {
            Vector3 mousePos = BoxDrawer.GetCurrentMouseWorldPosition();
            if (lastPos != mousePos) {
                currentLineRenderer.positionCount++;
                int positionIndex = currentLineRenderer.positionCount - 1;
                currentLineRenderer.SetPosition(positionIndex, mousePos);
                lastPos = mousePos;
            }
        }
    }
}
