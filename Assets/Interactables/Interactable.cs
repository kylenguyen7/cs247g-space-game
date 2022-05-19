using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] private float outlineWidth;

    private bool _outlined;

    protected void ToggleOutline(bool outline) {
        _outlined = outline;
        sprite.material.SetFloat("_Thickness", _outlined ? outlineWidth : 0f);
    }

    protected void ToggleOutline() {
        _outlined = !_outlined;
        sprite.material.SetFloat("_Thickness", _outlined ? outlineWidth : 0f);
    }
}
