using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

using GameFramework.Dialogue;

namespace GameFramework.UI
{
    public class DialogueManager : MonoBehaviour
    {

        public GameObject dialogueUIPrefab;
        //private CinemachineStateDrivenCamera _cinemachineVirtualCamera;

        private GameObject _dialogueUI;
        private SimpleDialogueView _dialogueView;
        private SimpleDialogue _simpleDialogue;
        private int _dialogueIndex;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            
            if (dialogueUIPrefab) {
                _dialogueUI = Instantiate(dialogueUIPrefab, transform);
                _dialogueUI.SetActive(false);
                _dialogueView = _dialogueUI.GetComponent<SimpleDialogueView>();
            }
            GameEventSystem.OnNextDialogueLine += ShowDialogueNextLine;
        }

        private void OnDestroy()
        {
            GameEventSystem.OnNextDialogueLine -= ShowDialogueNextLine;
        }

        public void ShowDialogue(SimpleDialogue dialogue)
        {
            _dialogueUI.SetActive(true);
            _simpleDialogue = dialogue;
            _dialogueIndex = 0;
            _dialogueView.ShowLine(_simpleDialogue.GetLines()[_dialogueIndex]);
        }

        private void ShowDialogueNextLine()
        {
            if (_dialogueIndex < _simpleDialogue.GetLines().Length)
            {
                _dialogueView.ShowLine(_simpleDialogue.GetLines()[_dialogueIndex]);
                _dialogueIndex++;
            }
            else {
                _dialogueIndex = 0;
                _simpleDialogue = null;
                _dialogueUI.SetActive(false);
                GameEventSystem.OnEndDialogue();
            }
        }
    }
}