using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientLeaderboard : MonoBehaviour
{
    Image Gradient;
    //public RectTransform ScrollView;
    public ScrollRect ScrollView;
    // Start is called before the first frame update
    void Start()
    {
        Gradient = GetComponent<Image>();
        Gradient.enabled = false;

    }

    private void Update() {
        Debug.Log(ScrollView.normalizedPosition.y);
        if (ScrollView.normalizedPosition.y < 0.99f) {
            Gradient.enabled = true;
        } else {
            Gradient.enabled = false;
        }
    }

}
