using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrLevelDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        GetComponent<TMPro.TMP_Text>().text = "AI\n" + "Level " + PlayerPrefs.GetString("Level");
    }

}
