using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeCodeWriter : MonoBehaviour
{
    public bool addFrontBit = true;
    // Start is called before the first frame update
    private void Awake() {
        GetComponent<TMPro.TMP_Text>().text = addFrontBit == true ?  "Employee Code: \n" : "" + PlayerPrefs.GetString("Alias");
    }
}
