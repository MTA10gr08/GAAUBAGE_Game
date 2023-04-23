using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeCodeWriter : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        GetComponent<TMPro.TMP_Text>().text = "Employee Code: \n" + PlayerPrefs.GetString("Alias");
    }
}
