using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Conversation", order = 1)]
public class ConversationScriptableObject : ScriptableObject
{
    public List<DialogueContainer> Dialogues = new List<DialogueContainer>();
}

[Serializable]
public class DialogueContainer
{
    public string name = "PlaceHolder";
    [TextArea(4, 10)] public string speech = "Hello!";
    [field: SerializeField] private textSpeed textSpeed = textSpeed.normal;
    public float TextSpeed {
        get {
            return textSpeed switch {
                textSpeed.slow => .05f,
                textSpeed.normal => .03f,
                textSpeed.fast => .01f,
                _ => .0f,
            };
        }
    }
    public Color textColor = Color.black;
}
public enum textSpeed
{
    slow,
    normal,
    fast,
    instant

}
