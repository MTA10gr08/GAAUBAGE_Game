using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class ContextSelector : MonoBehaviour
{
    //public string SelectedContext = "";
    public TMP_Dropdown contextDropdown;
    private void Awake() {
        PopulateDropDown();
    }
    void PopulateDropDown() {
        contextDropdown.ClearOptions();
        contextDropdown.AddOptions(Configuration.categories.ContextCategories.ToList());
    }

    public string SelectedContext() {

        return contextDropdown.options[contextDropdown.value].text;
    }
}

