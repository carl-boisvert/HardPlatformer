using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Dialogue
{
    [CreateAssetMenu(fileName = "SimpleDialogue", menuName = "Data/SimpleDialogue", order = 1)]
    public class SimpleDialogue : DialogueBase
    {
        [SerializeField]
        private DialogueLine[] lines;

        public DialogueLine[] GetLines() {
            return lines;
        }
    }
}