using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateUI : MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf == true) {
            gameObject.transform.Rotate(Vector3.forward, -360 * Time.deltaTime);
        }
    }
}
