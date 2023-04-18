using System.Collections;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
	TMPro.TMP_Text txt;
	int index = 0;
	public ConversationScriptableObject convo;
	bool skipping;

	void Awake() {
		txt = GetComponent<TMPro.TMP_Text>();
		story = txt.text;
		txt.text = "";

		//NextDialogue();
	}
	IEnumerator PlayText() {
		foreach (char c in convo.Dialogues[index].speech) {
			txt.text += c;
            if (skipping) {
				yield return null;
            } else {
				yield return new WaitForSeconds(convo.Dialogues[index].TextSpeed);
            }
		}
		index++;
	}
	[ContextMenu("NextDialogue")]
	void NextDialogue()
    {
        if (index >= convo.Dialogues.Count) {
			//Finish Convo if no more dialogue
			return;
        }
		txt.text = convo.Dialogues[index].name + ": ";
		StartCoroutine(PlayText());
    }

}
