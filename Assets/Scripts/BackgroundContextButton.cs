using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundContextButton : MonoBehaviour
{
    public Button button;
    public bool IsSelected = false;
    public Sprite SelectedImage, UnSelectedImage;
    //private int selectedColor = 100, unSelectedColor = 75;
    [SerializeField] public Image SelectionStatusImage = null;
    Color32 selectedColor = new Color32(255, 255, 255, 255), unSelctedColor = new Color32(200, 200, 200, 255);

    Image buttonImage;

    private void Awake() {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        //buttonImage.color = Color.HSVToRGB(0,0,unSelectedColor);
        button.onClick.AddListener(delegate { ChangeSelection(); });
        //SelectionStatusImage = button.GetComponentInChildren<Image>();
        SelectionStatusImage.sprite = UnSelectedImage;
    }

    void ChangeSelection() {
        IsSelected = !IsSelected;

        if (IsSelected == true) {
            //var color = Color.HSVToRGB(0, 0, selectedColor);
            buttonImage.color = selectedColor;
            SelectionStatusImage.sprite = SelectedImage;
        } else {
            //var color = Color.HSVToRGB(0, 0, unSelectedColor);
            buttonImage.color = unSelctedColor;
            SelectionStatusImage.sprite = UnSelectedImage;
        }
    }
}
