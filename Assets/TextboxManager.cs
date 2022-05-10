using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextboxManager : MonoBehaviour {
    [SerializeField] private GameObject textboxPrefab;
    public static TextboxManager Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    public void CreateText(DialogueItem dialogue) {
        var textbox = Instantiate(textboxPrefab, transform).GetComponent<TextboxController>();
        textbox.Init(dialogue);
    }
}
