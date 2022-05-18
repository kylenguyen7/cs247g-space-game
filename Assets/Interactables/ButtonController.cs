using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ButtonController : Interactable {
    [SerializeField] private Sprite button;
    [SerializeField] private Sprite pressedButton;
    [SerializeField] private AudioClip onButtonDownSfx;
    [SerializeField] private AudioClip onButtonUpSfx;

    protected bool _active = true;
    private bool _hovered;
    private bool _pressed;

    private void OnMouseOver() {
        if (_active && !_hovered) {
            ToggleOutline(true);
            _hovered = true;
        }
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
                GlobalAudio.Source.PlayOneShot(onButtonUpSfx);
            }
            _pressed = false;
        }

        sprite.sprite = _hovered && _pressed ? pressedButton : button;
    }

    protected abstract void OnButtonPressed();
}
