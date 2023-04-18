using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMask : MonoBehaviour
{
    public void updateImageMask(float width, float height) {
        transform.position = Camera.main.transform.position;
        transform.position += new Vector3(0, 0, 11);

        transform.localScale = new Vector3(width, height, 1);
    }
}
