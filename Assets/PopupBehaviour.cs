using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBehaviour : MonoBehaviour
{
    public AnimationCurve animationCurve;
    bool Animate = false;
    float time;
    public float timeLength = 3f;

    private void Update() {
        if (Animate || time !<= 0) {
            transform.position = new Vector3(0, animationCurve.Evaluate(time), 0);
            time -= Time.deltaTime;
        } else {
            time = timeLength;
        }
    }
    private void OnEnable() {
        time = timeLength;
    }

}
