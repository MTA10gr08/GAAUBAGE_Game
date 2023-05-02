using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public TMPro.TMP_Text speechBox, nameBox;
    public Sprite nextSprite, endSprite;
    int index = 0;
    public List<ConversationScriptableObject> convos = new List<ConversationScriptableObject>();
    ConversationScriptableObject convo;
    bool writing;
    Coroutine writer;
    public Image ButtonIcon;
    public GameObject SpriteObject, Parent, AIFacePrefab;

    void Awake() {
        //speech = GetComponent<TMPro.TMP_Text>();
        //speechBox.text = "";
        //nameBox.text = "";
        convo = convos[PlayerPrefs.GetInt("Level")];
    }

    IEnumerator PlayText() {
        foreach (char c in convo.Dialogues[index].speech) {
            speechBox.text += c;
            yield return new WaitForSeconds(convo.Dialogues[index].TextSpeed);
        }
        index++;
        writing = false;
    }

    [ContextMenu("NextDialogue")]
    public void NextDialogue() {
        if (index + 1 == convo.Dialogues.Count) {
            //Finish Convo if no more dialogue
            ButtonIcon.sprite = endSprite;
        }

        if (index >= convo.Dialogues.Count) {
            //Finish Convo if no more dialogue
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Narrative_Home");
            return;
        }

        nameBox.text = convo.Dialogues[index].name == "[You]" ? PlayerPrefs.GetString("Alias") : convo.Dialogues[index].name;
        nameBox.fontSharedMaterial = convo.Dialogues[index].fontMaterial;
        nameBox.color = convo.Dialogues[index].textColor;
        if (writing) {
            StopCoroutine(writer);
            speechBox.text = convo.Dialogues[index].speech;
 
            index++;
            writing = false;
            return;
        }

        writing = true;
        speechBox.text = string.Empty;
        speechBox.fontSharedMaterial = convo.Dialogues[index].fontMaterial;
        speechBox.color = convo.Dialogues[index].textColor;

        if (convo.Dialogues[index].AnimatedSprite != null) {
            Destroy(SpriteObject);
            SpriteObject = Instantiate(convo.Dialogues[index].AnimatedSprite, Parent.transform);
            SpriteObject.transform.SetAsFirstSibling();
        } else if (convo.Dialogues[index].StillSprite != null) {
            Destroy(SpriteObject);
            SpriteObject = Instantiate(convo.Dialogues[index].StillSprite, Parent.transform);
            SpriteObject.transform.SetAsFirstSibling();
        } else if(convo.Dialogues[index].AIFace != "") {
            Destroy(SpriteObject);
            SpriteObject = Instantiate(AIFacePrefab, Parent.transform);
            SpriteObject.transform.SetAsFirstSibling();
            SpriteObject.GetComponent<AIFaceObject>().SetFace(convo.Dialogues[index].AIFace);
        } else {
            //Diplay nothing
        }
        
        writer = StartCoroutine(PlayText());
    }

}
