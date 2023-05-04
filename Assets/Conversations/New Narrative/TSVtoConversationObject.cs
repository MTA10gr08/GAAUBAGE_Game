using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TSVtoConversationObject : MonoBehaviour
{
    public TextAsset tsvFile;
    public List<DialogueContainer> dialogueEntries;

    [ContextMenu("test")]
    public void test()
    {
        string[] lines = tsvFile.text.Split('\n');
        dialogueEntries = new List<DialogueContainer>();

        for (int i = 1; i < lines.Length; i++)
        {
            if (!string.IsNullOrEmpty(lines[i]))
            {
                string[] values = lines[i].Split('\t');
                DialogueContainer entry = new DialogueContainer
                {
                    name = values[0],
                    speech = values[1]
                };
                dialogueEntries.Add(entry);
            }
        }

        AssetDatabase.CreateAsset(new ConversationScriptableObject()
        {
            Dialogues = dialogueEntries
        }, "Assets/Conversations/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();
    }
}
