using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextboxManager : MonoBehaviour {
    [SerializeField] private GameObject textboxPrefab;
    [SerializeField] private DialogueItem firstCitation;
    [SerializeField] private DialogueItem secondCitation;
    [SerializeField] private DialogueItem finalCitation;
    public static TextboxManager Instance;
    private Queue<DialogueItem> _dialogueItems;
    private TextboxController _currentTextbox; 

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _dialogueItems = new Queue<DialogueItem>();
    }
    
    public void QueueText(DialogueItem dialogue) {
        _dialogueItems.Enqueue(dialogue);
    }

    public void QueueCitation() {
        switch (CitationsManager.Instance.NumCitations) {
            case 0: {
                _dialogueItems.Enqueue(firstCitation);
                break;
            }
            case 1: {
                _dialogueItems.Enqueue(secondCitation);
                break;
            }
            case 2: {
                _dialogueItems.Clear();
                _dialogueItems.Enqueue(finalCitation);
                break;
            }
        }
    }

    public void ClearDialogueQueue() {
        _dialogueItems.Clear();
    }

    private void Update() {
        if (_currentTextbox == null && _dialogueItems.Count > 0) {
            var dialogue = _dialogueItems.Dequeue();
            _currentTextbox = Instantiate(textboxPrefab, transform).GetComponent<TextboxController>();
            _currentTextbox.Init(dialogue);
        }
    }
}
