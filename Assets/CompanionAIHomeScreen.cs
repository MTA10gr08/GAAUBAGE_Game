using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CompanionAIHomeScreen : MonoBehaviour
{
    public TMPro.TMP_Text AIFace, AISpeech;
    List<string> faces = new List<string> { "  ° o °", "  ° v °", "  ° > °" };
    List<string> speech = new List<string> { "", "Hello", "How are you doing?", "Ready to work?", "Good Morning", "Good evening", "Good Evening" };
    private void Start() {
        int selector = UnityEngine.Random.Range(0, speech.Count);
        var text = string.Empty;

        if (selector == 0) {
            switch (PlayerPrefs.GetInt("Level")) {
                default:
                    text = "Remember to train my model.";
                    break;
                case 1:
                    text = "";
                    break;
                case 2:
                    text = "Jeremiah ";
                    break;
                case 3:
                    text = "";
                    break;
                    //case 4:
                    //    break;
            }
        } else if (selector >= speech.Count - 3) {
            switch (DateTime.Now.Hour) {
                case < 12:
                    text = speech[speech.Count - 3];
                    break;
                default:
                    text = speech[speech.Count - 2];
                    break;
                case > 18:
                    text = speech[speech.Count - 1];
                    break;
            }
        } else {
            text = speech[selector];
        }
        AIFace.text = faces[UnityEngine.Random.Range(0, faces.Count)];
        StartCoroutine(PlayText(text));
    }

    IEnumerator PlayText(string speech) {
        yield return new WaitForSeconds(4f);
        foreach (char c in speech) {
            AISpeech.text += c;
            yield return new WaitForSeconds(.05f);
        }
    }
}
