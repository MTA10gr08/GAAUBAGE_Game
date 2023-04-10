using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFitter : MonoBehaviour
{
    public float maxHeight, maxWidth;
    // Start is called before the first frame update
    void Start()
    {
        //var image = GetComponent<UnityEngine.UI.Image>();
        UpdateImage();

    }
    [ContextMenu("Update image")]
    public void UpdateImage() {

        Vector2 size = GetComponent<Image>().sprite.rect.size;
        float scaler;
        if (size.x*2 < size.y) {
            scaler = maxHeight / size.y;
        } else {
            scaler = maxWidth / size.x;
        }
        Vector2 adjustedSize = new Vector2(size.x * scaler, size.y * scaler);
        GetComponent<RectTransform>().sizeDelta = adjustedSize;
        //GetComponent<LayoutElement>().preferredHeight = GetComponent<Image>().sprite.rect.size.y;
    }

    [ContextMenu("Real Size")]
    public void SetNativeSize() {
        GetComponent<RectTransform>().sizeDelta = GetComponent<Image>().sprite.rect.size;
    }

}
