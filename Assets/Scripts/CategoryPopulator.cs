using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CategoryPopulator : MonoBehaviour
{
    public TextAsset jsonData;

    public Button SubmitBtn;
    public TMP_Dropdown superCategoryDropdown, categoryDropdown;
    private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
    private string defaultDropdownOption = "Select";

    void Start() {
        //Make a dictionary from json text
        Rootobject rootobject = JsonUtility.FromJson<Rootobject>(jsonData.ToString());
        data = rootobject.TrashCategories.ToDictionary(x => x.Name, x => x.SubCategories.ToList());
            //string json = jsonData.ToString();
            //trashCategories = JsonUtility.FromJson<Rootobject>(json);
            //Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

            //foreach (TrashCategory category in trashCategories.TrashCategories) {
                //data[category.Name] = category.SubCategories.ToList();
            //}
        // Populate super category dropdown
        List<string> superCategories = new List<string>(data.Keys);
        superCategories.Insert(0, defaultDropdownOption);
        Debug.Log(superCategories[0]);
        superCategoryDropdown.ClearOptions();
        superCategoryDropdown.AddOptions(superCategories);
        //superCategoryDropdown.value = superCategoryDropdown.options.Count;

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
}

[System.Serializable]
public class Rootobject
{
    public List<TrashCategory> TrashCategories;
}
[System.Serializable]
public class TrashCategory
{
    public string Name;
    public string[] SubCategories;
}


