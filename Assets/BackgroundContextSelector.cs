using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundContextSelector : MonoBehaviour
{
    public List<BackgroundContextButton> buttons = new List<BackgroundContextButton>();
    List<string> categories = new List<string>();
    List<string> categoriesToSubmit = new List<string>();

    private void Awake() {
        categories.Add("category 1");
        categories.Add("category 2");
        categories.Add("category 3");
        categories.Add("category 4");
        categories.Add("category 5");
        categories.Add("category 6");
        categories.Add("category 7");
    }

    void AssignCategory(bool category) {
        category = !category;
        if (category) {

        }
    }
    [ContextMenu("Compile String List")]
    void CompileStringList() {
        int indexer = 0;
        foreach (var item in buttons) {
            if (item.IsSelected == true) {
                categoriesToSubmit.Add(categories[indexer]);
            }
            indexer++;
        }
        Debug.Log(categoriesToSubmit.Count);
    }

}
