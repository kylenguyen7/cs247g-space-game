using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonController : Interactable {
    [SerializeField] private GameObject packagePrefab;
    [SerializeField] private Sprite button;
    [SerializeField] private Sprite pressedButton;

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
            _pressed = true;
        }

        if (Input.GetMouseButtonUp(0)) { 
            if (_hovered && _pressed) {
                SpawnPackage();
            }
            _pressed = false;
        }

        sprite.sprite = _hovered && _pressed ? pressedButton : button;
    }

    private void SpawnPackage() {
        if (FindObjectOfType<PackageController>() == null) {
            Instantiate(packagePrefab, new Vector2(0, 18f), Quaternion.identity);
        }
    }
}
