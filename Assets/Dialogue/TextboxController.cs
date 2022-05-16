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
    [SerializeField] private AudioClip onChatSfx1;
    [SerializeField] private AudioClip onChatSfx2;
    [SerializeField] private GameObject endOfDayButton;
    private DialogueItem _dialgoueItem;
    
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
        _dialgoueItem = dialogue;

        if (!dialogue.EndOfDay) {
            endOfDayButton.SetActive(false);
        }
        image.sprite = dialogue.Image;
        nameField.text = dialogue.Name;
        
        _text = dialogue.Text;
        CurrentText = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText() {
        while (CurrentText.Length < _text.Length) {
            if (CurrentText.Length % 2 == 0 ){
                if (UnityEngine.Random.Range(1, 3) >= 1.5){
                    GlobalAudio.Source.PlayOneShot(onChatSfx1);
                } else {
                    GlobalAudio.Source.PlayOneShot(onChatSfx2);
                }
            }
            CurrentText += _text[CurrentText.Length];
            yield return new WaitForSeconds(Input.GetMouseButton(0) ? fastTypeDelay : defaultTypeDelay);
        }

        _finished = true;
    }

    private void Update() {
        if (_finished && Input.GetMouseButtonDown(0) && !_dialgoueItem.EndOfDay) {
            Destroy(gameObject);
        }
    }

    public void OnEndDayButtonClicked() {
        DayEndController.Instance.EndDay();
        Destroy(gameObject);
    }
}
