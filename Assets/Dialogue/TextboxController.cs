using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextboxController : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private float defaultTypeDelay;
    [SerializeField] private float fastTypeDelay;
    
    private String _text;
    private String _currentText;

    private String CurrentText {
        get => _currentText;
        set {
            textField.text = value;
            _currentText = value;
        }
    }
    private bool _finished;
    
    public void Init(DialogueItem dialogue) {
        image.sprite = dialogue.Image;
        nameField.text = dialogue.Name;
        
        _text = dialogue.Text;
        CurrentText = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText() {
        while (CurrentText.Length < _text.Length) {
            CurrentText += _text[CurrentText.Length];
            yield return new WaitForSeconds(Input.GetMouseButton(0) ? fastTypeDelay : defaultTypeDelay);
        }

        _finished = true;
    }

    private void Update() {
        if (_finished && Input.GetMouseButtonDown(0)) {
            Destroy(gameObject);
        }
    }
}
