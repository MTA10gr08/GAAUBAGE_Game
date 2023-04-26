using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using GAAUBAGE_Game.API.Models;
using System.Linq;

public class BoxManager : MonoBehaviour
{
    public GameObject BoxObjectPrefab, BoxCanvas;
    List<BoxObject> BoxObjects = new List<BoxObject>();
    public Button NewBoxBtn, SubmitButton;
    public SpriteRenderer spriteRendere;

    private void Awake() {
        NewBoxBtn.onClick.AddListener(delegate { MakeNewBox(); });
        SubmitButton.interactable = false;
    }
    void MakeNewBox() {
        var tmp = Instantiate(BoxObjectPrefab, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity, BoxCanvas.transform);
        BoxObject boxObject = tmp.GetComponent<BoxObject>();
        boxObject.boxManager = this;
        BoxObjects.Add(boxObject);
        if (!SubmitButton.interactable) {
            SubmitButton.interactable = true;
        }
    }
    public void RemoveBox(BoxObject boxObject) {
        BoxObjects.Remove(boxObject);
        Destroy(boxObject.gameObject);
    }

    public List<SubImageAnnotation> ReturnBoxes() {
        List<SubImageAnnotation> boxes = new List<SubImageAnnotation>();

        foreach (var box in BoxObjects) {
            SubImageAnnotation boundingBox = new SubImageAnnotation();
            var vector = box.WorldSpacePoints().OrderBy(x => x.magnitude);
            boundingBox.X = (uint)Mathf.Clamp(vector.First().x, 0, spriteRendere.sprite.texture.width);
            boundingBox.Y = (uint)-Mathf.Clamp(vector.First().y, -spriteRendere.sprite.texture.height, 0);
            boundingBox.Width = (uint)Mathf.Clamp(vector.Last().x - vector.First().x, 0, spriteRendere.sprite.texture.width);
            boundingBox.Height = (uint)Mathf.Clamp(vector.First().y - vector.Last().y, 0, spriteRendere.sprite.texture.height);
            boxes.Add(boundingBox);
        }
        return boxes;
    }
    public void ClearBoxes() {
        foreach (var item in BoxObjects) {
            Destroy(item.gameObject);
        }
        BoxObjects.Clear();
    }
}
