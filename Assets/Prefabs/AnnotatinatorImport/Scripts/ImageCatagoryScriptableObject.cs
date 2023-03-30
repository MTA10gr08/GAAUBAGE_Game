using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ImageCatagoryScriptableObject", order = 1)]
public class ImageCatagoryScriptableObject : ScriptableObject
{
    public string Name;
    public Color Color;
}
