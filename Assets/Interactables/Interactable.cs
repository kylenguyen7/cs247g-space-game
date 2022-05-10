using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] private float outlineWidth;

    protected void ToggleOutline(bool outline) {
        sprite.material.SetFloat("_Thickness", outline ? outlineWidth : 0f);
    }
}
