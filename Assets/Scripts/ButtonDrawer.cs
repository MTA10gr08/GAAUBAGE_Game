using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDrawer : MonoBehaviour
{
    public Button taskButton, burgerButton;
    public List<GameObject> taskButtons, burgerButtons;

    private void Start() {
        taskButton.onClick.AddListener( delegate { DrawButtons(taskButtons, burgerButtons); });
        burgerButton.onClick.AddListener(delegate { DrawButtons(burgerButtons, taskButtons); });
        foreach (var item in taskButtons) {
            item.SetActive(false);
        }
        foreach (var item in burgerButtons) {
            item.SetActive(false);
        }
    }
    void DrawButtons(List<GameObject> open, List<GameObject> close) {
        foreach (var item in open) {
            if (item.activeSelf) {
                item.SetActive(false);
            } else {
                item.SetActive(true);
            }
        }
        foreach (var item in close) {
            item.SetActive(false);
        }
    }

}
