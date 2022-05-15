using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonController : Interactable {
    [SerializeField] private Sprite button;
    [SerializeField] private Sprite pressedButton;
    [SerializeField] private AudioClip onButtonDownSfx;
    [SerializeField] private AudioClip onButtonUpSfx;

    private bool _hovered;
    private bool _pressed;

    private void OnMouseEnter() {
        ToggleOutline(true);
        _hovered = true;
    }

    private void OnMouseExit() {
        ToggleOutline(false);
        _hovered = false;
    }

    private void Update() {
        if (_hovered && Input.GetMouseButtonDown(0)) {
            GlobalAudio.Source.PlayOneShot(onButtonDownSfx);
            _pressed = true;
        }

        if (Input.GetMouseButtonUp(0)) { 
            if (_hovered && _pressed) {
                OnButtonPressed();
            }
            _pressed = false;
        }

        sprite.sprite = _hovered && _pressed ? pressedButton : button;
    }

    private void OnButtonPressed() {
        GlobalAudio.Source.PlayOneShot(onButtonUpSfx);
        
        if (FindObjectOfType<PackageController>() == null) {
            GameObject nextPackage = PackageData.Instance.GetNextPackage();
            if (nextPackage == null) return;
            Instantiate(nextPackage, new Vector2(0f, -18f), Quaternion.identity);
        }
    }
}
