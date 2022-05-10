
using System;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Interactable {
    [SerializeField] protected Rigidbody2D rb; 
    private bool _holding;
    protected bool Holding => _holding;
    private Vector2 _holdingOffset;
    
    private void OnMouseEnter() {
        ToggleOutline(true);
    }

    private void OnMouseExit() {
        if (!_holding) {
            ToggleOutline(false);
        }
    }

    private void OnMouseDown() {
        _holding = true;
        _holdingOffset = GetMousePosWorldCoordinates() - (Vector2)transform.position;
    }

    protected void OnMouseUp() {
        _holding = false;
    }
    
    protected void Update() {
        if (_holding) {
            rb.MovePosition(GetMousePosWorldCoordinates() - _holdingOffset);
        }
    }

    protected static Vector2 GetMousePosWorldCoordinates() {
        Vector2 pos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(pos);
    }
}