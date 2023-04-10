using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCornerHandle : MonoBehaviour
{
    public GameObject myBox;
    public void OnTap() {
        Destroy(myBox);
    }
    public void OnDrag() {
        transform.position = BoxDrawer.GetCurrentMouseWorldPosition();
    }
}
