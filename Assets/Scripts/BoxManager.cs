using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using Assets.Scripts.API.Models;
using System.Linq;

public class BoxManager : MonoBehaviour
{
    public GameObject BoxObjectPrefab, BoxCanvas;
    List<BoxObject> BoxObjects = new List<BoxObject>();
    public Button NewBoxBtn, SubmitButton;
    public Image image;

    private void Awake()
    {
        NewBoxBtn.onClick.AddListener(delegate { MakeNewBox(); });
        SubmitButton.interactable = false;
    }
    void MakeNewBox()
    {
        var tmp = Instantiate(BoxObjectPrefab, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity, BoxCanvas.transform);
        BoxObject boxObject = tmp.GetComponent<BoxObject>();
        boxObject.boxManager = this;
        //boxObject.Min = image
        BoxObjects.Add(tmp.GetComponent<BoxObject>());
        if (!SubmitButton.interactable)
        {
            SubmitButton.interactable = true;
        }
    }
    public void RemoveBox(BoxObject boxObject)
    {
        BoxObjects.Remove(boxObject);
        Destroy(boxObject.gameObject);
    }

    public List<BoundingBox> ReturnBoxes()
    {
        return BoxObjects.ConvertAll(x => new BoundingBox
        {
            X = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.x),
            Y = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.y),
            Width = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.x - x.BoxPoints[1].transform.position.x),
            Height = (uint)Mathf.Ceil(x.BoxPoints[0].transform.position.y - x.BoxPoints[2].transform.position.y)
        }).ToList();
    }
}
