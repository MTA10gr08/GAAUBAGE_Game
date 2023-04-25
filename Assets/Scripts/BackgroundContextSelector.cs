using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BackgroundContextSelector : MonoBehaviour
{
    public List<BackgroundContextButton> buttons = new List<BackgroundContextButton>();
    List<string> categories = new List<string>();
    List<string> categoriesToSubmit = new List<string>();

    private void Awake() {
        categories = (Configuration.categories.BackgroundCategories.ToList());
    }

    [ContextMenu("Compile String List")]
    public List<string> CompileStringList() {
        int indexer = 0;
        categoriesToSubmit.Clear();
        foreach (var item in buttons) {
            if (item.IsSelected == true) {
                categoriesToSubmit.Add(categories[indexer]);
            }
            indexer++;
        }
        return categoriesToSubmit;
    }
    public void ClearSelection() {
        foreach (var item in buttons) {
            if (item.IsSelected == true) {
                item.GetComponent<BackgroundContextButton>().ChangeSelection();
            }
        }
        categoriesToSubmit.Clear();
    }

}
