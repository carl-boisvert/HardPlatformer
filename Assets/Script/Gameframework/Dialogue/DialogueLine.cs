using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameFramework.Entities;

namespace GameFramework.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueLine", menuName = "Data/DialogueLine", order = 1)]
    public class DialogueLine : ScriptableObject
    {
        [SerializeField]
        private Speaker speaker;
        [SerializeField]
        private string line;


        public Sprite GetAvatar() {
            return speaker.avatar;
        }

        public string GetSpeakerName() {
            return speaker.characterName;
        }

        public string GetDialogueLine() {
            return line;
        }

    }
}