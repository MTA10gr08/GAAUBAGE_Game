using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : MonoBehaviour
{
    public void ToggleGameObject()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void ToggleOff() {
        gameObject.SetActive(false);
    }
    public void ToggleOn() {
        gameObject.SetActive(true);
    }
}
