using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;

public class BoxManager : MonoBehaviour
{
    public GameObject BoxObjectPrefab, BoxCanvas;
    List<BoxObject> BoxObjects = new List<BoxObject>();
    public Button NewBoxBtn, SubmitButton;
    public Image image;

    private void Awake() {
        NewBoxBtn.onClick.AddListener(delegate { MakeNewBox(); });
        SubmitButton.interactable = false;
    }
    void MakeNewBox() {
        var tmp = Instantiate(BoxObjectPrefab, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity, BoxCanvas.transform);
        BoxObject boxObject = tmp.GetComponent<BoxObject>();
        boxObject.boxManager = this;
        //boxObject.Min = image
        BoxObjects.Add(tmp.GetComponent<BoxObject>());
        if (!SubmitButton.interactable) {
            SubmitButton.interactable = true;
        }
    }
    public void RemoveBox(BoxObject boxObject) {
        BoxObjects.Remove(boxObject);
        Destroy(boxObject.gameObject);
    }

    List<Rectangle> ReturnBoxes() {
        int indexer = 0;
        List<Rectangle> boxes = new List<Rectangle>();
        foreach ( var box in BoxObjects) {
            //Rectangle rect = new Rectangle();
            //rect.Width = box.BoxPoints[0].transform.position.x - box.BoxPoints[1].transform.position.x;
            //rect.Height = 
            //box.BoxPoints
            //indexer++;
        }

        return boxes;
    }
}
