using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLineWithCamera : MonoBehaviour
{
    public float Scale = 0.1f;
    LineRenderer rendere;

    private void Awake() {
        rendere = GetComponent<LineRenderer>();
        rendere.startWidth = (Camera.main.orthographicSize * 2 * Scale);
    }

    private void Update() {
        rendere.startWidth = (Camera.main.orthographicSize * 2 * Scale);
    }
}
