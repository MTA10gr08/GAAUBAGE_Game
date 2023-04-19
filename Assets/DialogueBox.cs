using System.Collections;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public TMPro.TMP_Text speechBox, nameBox;
    int index = 0;
    public ConversationScriptableObject convo;
    bool writing;
    Coroutine writer;

    void Awake() {
        //speech = GetComponent<TMPro.TMP_Text>();
        speechBox.text = "";
        nameBox.text = "";
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
        if (index >= convo.Dialogues.Count) {
            //Finish Convo if no more dialogue
            return;
        }
        nameBox.text = convo.Dialogues[index].name;

        if (writing) {
            StopCoroutine(writer);
            speechBox.text = convo.Dialogues[index].speech;
            index++;
            writing = false;
            return;
        }



        writing = true;
        speechBox.text = "";
        writer = StartCoroutine(PlayText());
    }

}
