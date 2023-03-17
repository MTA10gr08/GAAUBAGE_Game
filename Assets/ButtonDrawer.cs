using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDrawer : MonoBehaviour
{
    public List<GameObject> TaskButtons, ScreenButtons;
    bool taskButtonsAreOpen = false, screenButtonsAreOpen = false;


    public void OpenOrClose(bool isTasks) {
        if (isTasks) {
            if (!taskButtonsAreOpen) {
                foreach (var item in TaskButtons) {
                    item.SetActive(true);
                }
            } else {

            }
        } else {

        }
    }
}
