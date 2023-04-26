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
    public SpriteRenderer imagegeg;
    //public Image image;

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
        //return BoxObjects.ConvertAll(x => new BoundingBox
        //{
        //    X = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.x),
        //    Y = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.y),
        //    Width = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.x - x.BoxPoints[1].transform.position.x),
        //    Height = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.y - x.BoxPoints[2].transform.position.y)
        //}).ToList();
        foreach (var box in BoxObjects) {
            SubImageAnnotation boundingBox = new SubImageAnnotation();
            var vector = box.WorldSpacePoints().OrderBy(x => x.magnitude);
            Debug.Log(vector.First().magnitude + " | " + vector.Last().magnitude);
            Debug.Log(vector.First() + " | " + vector.Last());

            boundingBox.X = (uint)Mathf.Clamp(vector.First().x, 0, imagegeg.sprite.texture.width);
            boundingBox.Y = (uint)-Mathf.Clamp(vector.First().y, -imagegeg.sprite.texture.height, 0);
            Debug.Log(boundingBox.X + " x|y " + boundingBox.Y);
            boundingBox.Width = (uint)Mathf.Clamp(vector.Last().x - vector.First().x, 0, imagegeg.sprite.texture.width);
            boundingBox.Height = (uint)Mathf.Clamp(vector.First().y - vector.Last().y, 0, imagegeg.sprite.texture.height);
            Debug.Log(boundingBox.Width + " W|H " + boundingBox.Height);
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
