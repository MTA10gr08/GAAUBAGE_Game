using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFaceObject : MonoBehaviour
{
    public TMPro.TMP_Text face;

    public void SetFace(string newface) {
        face.text = newface;
    }
}
