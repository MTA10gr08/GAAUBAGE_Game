using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class ContextSelector : MonoBehaviour
{
    public TMP_Dropdown contextDropdown;
    private void Awake() {
        PopulateDropDown();
    }
    void PopulateDropDown() {
        contextDropdown.ClearOptions();
        var list = Configuration.categories.ContextCategories.ToList();
        list.Insert(0, "Select");
        contextDropdown.AddOptions(list);
    }

    public string SelectedContext() {
        return contextDropdown.options[contextDropdown.value].text;
    }
    public void ClearSelection() {
        contextDropdown.value = 0;
        contextDropdown.RefreshShownValue();
    }
}

