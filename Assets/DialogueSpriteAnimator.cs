using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSpriteAnimator : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public float timeBetweenSprites = .5f;
    private float currentTime = 0;
    int index;
    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        currentTime -= Time.deltaTime;
        Debug.Log(index);
        if (currentTime <= 0) {
            index++;
            index = index % sprites.Count;
            currentTime = timeBetweenSprites;
            image.sprite = sprites[index];
        }

    }

}
