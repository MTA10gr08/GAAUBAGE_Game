using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientLeaderboard : MonoBehaviour
{
    Image GradientContainer;

    //public RectTransform ScrollView;
    public ScrollRect ScrollView;
    //public Sprite Gradient;
 
    // Start is called before the first frame update
    void Start()
    {
        GradientContainer = GetComponent<Image>();
        GradientContainer.enabled = false;

    }

    private void Update() {
        if (ScrollView.normalizedPosition.y < 0.99f) {
            //GradientContainer.sprite = Gradient;
            GradientContainer.enabled = true;
        } else {
            GradientContainer.enabled = false;
            //GradientContainer.sprite = null;
        }
    }

}
