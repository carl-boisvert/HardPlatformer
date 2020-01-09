using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework.UI;

namespace GameFramework.Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        public bool _isOneTime = false;
        [SerializeField]
        private SimpleDialogue _dialogue;

        private Collider2D _collider2D;
        private DialogueManager _dialogueManager;

        // Start is called before the first frame update
        void Start()
        {
            _collider2D = GetComponent<Collider2D>();
            _collider2D.isTrigger = true;
            _dialogueManager = FindObjectOfType<DialogueManager>();
            if (_dialogueManager == null) {
                UnityEngine.Debug.LogError("Couldn't find DialogueManager");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player") {
                GameEventSystem.OnStartDialogue();
                _dialogueManager.ShowDialogue(_dialogue);
                Destroy(gameObject);
            }
        }
    }
}