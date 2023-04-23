using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAccessController : MonoBehaviour
{
    //Level needs to be adjusted by server somehow
    [Range(0, 3)] public int level;
    public List<Button> taskButtons = new List<Button>();
    void Start()
    {
        for (int i = 0; i < taskButtons.Count; i++) {
            
            if (level >= i) {
                taskButtons[i].interactable = true;
            } else {
                taskButtons[i].interactable = false;
            }

        }
    }

}
