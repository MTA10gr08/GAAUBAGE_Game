using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CompanionAIHomeScreen : MonoBehaviour
{
    public TMPro.TMP_Text AIFace, AISpeech;
    List<string> faces = new List<string> { "-  ° o °", "-  ° v °", "-  ° > °" };
    List<string> speech = new List<string> { "Hello", "How are you doing?", "Ready to work?", "Good Morning", "Good evening", "Good Evening" };
    private void Start() {
        int selector = UnityEngine.Random.Range(0, speech.Count);


        if (selector >= speech.Count - 3) {
            switch (DateTime.Now.Hour) {
                case < 12:
                    AISpeech.text = speech[speech.Count - 3];
                    break;
                default:
                    AISpeech.text = speech[speech.Count - 2];
                    break;
                case > 18:
                    AISpeech.text = speech[speech.Count - 1];
                    break;
            }
        } else {
            AISpeech.text = speech[selector];
        }
        AIFace.text = faces[UnityEngine.Random.Range(0, faces.Count)];
    }
}
