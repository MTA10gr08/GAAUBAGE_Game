using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetLevel : MonoBehaviour
{
    public LevelAccessController lac;
    TMPro.TMP_Text lvl;

    private void Awake() {
        lvl = GetComponent<TMPro.TMP_Text>();
        lvl.text = "" + PlayerPrefs.GetInt("Level");
    }
    private void Update() {
        if (lvl.text != ""+PlayerPrefs.GetInt("Level")) {
            lvl.text = ""+PlayerPrefs.GetInt("Level");
        }
    }
}
