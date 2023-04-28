using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CategoryPopulator : MonoBehaviour
{
    public Button SubmitBtn;
    public TMP_Dropdown superCategoryDropdown, categoryDropdown;
    private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
    private string defaultDropdownOption = "Select";

    void Start() {
        //Make a dictionary from json text
        data = Configuration.categories.TrashCategories.ToDictionary(x => x.Name, x => x.SubCategories.ToList());

        List<string> superCategories = new List<string>(data.Keys);
        superCategories.Insert(0, defaultDropdownOption);
        superCategoryDropdown.ClearOptions();
        superCategoryDropdown.AddOptions(superCategories);

        // Populate category dropdown based on selected super category
        superCategoryDropdown.onValueChanged.AddListener(delegate { SubmitBtn.interactable = false; OnSuperCategoryDropdownValueChanged(); });
        categoryDropdown.onValueChanged.AddListener(delegate { OnCategoryDropdownValueChanged(); });
    }

    void OnSuperCategoryDropdownValueChanged() {
        // Get selected super category
        string selectedSuperCategory = "";
        if (superCategoryDropdown.options.Count <= 0) {

            return;
        }
        if (superCategoryDropdown.value == 0) {
            categoryDropdown.ClearOptions();
            categoryDropdown.RefreshShownValue();
            categoryDropdown.value = 0;
            categoryDropdown.interactable = false;
            return;
        }

        selectedSuperCategory = superCategoryDropdown.options[superCategoryDropdown.value].text;

        if (!data.ContainsKey(selectedSuperCategory)) {
            return;
        }

        List<string> categories = new List<string>(data[selectedSuperCategory]);
        categoryDropdown.interactable = (categories.Count > 1);
        categoryDropdown.ClearOptions();
        categories.Insert(0, defaultDropdownOption);
        categoryDropdown.AddOptions(categories);

        if (!categoryDropdown.interactable) {
            categoryDropdown.value = categoryDropdown.options.Count();
        }


    }
    void OnCategoryDropdownValueChanged() {

        if (categoryDropdown.value > 0) {
            SubmitBtn.interactable = true;
            return;
        }
        SubmitBtn.interactable = false;

    }
    public void ClearSelection() {
        categoryDropdown.value = 0;
        categoryDropdown.RefreshShownValue();
        superCategoryDropdown.value = 0;
        superCategoryDropdown.RefreshShownValue();
    }

}


