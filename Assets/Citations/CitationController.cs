using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class CitationController : Draggable {
    [SerializeField] private Vector2 enterPosition;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI body;
    [SerializeField] private Collider2D collider;
    [SerializeField] private Canvas canvas;

    private bool _entering = true;

    public void Init(String citationText) {
        switch (CitationsManager.Instance.NumCitations) {
            case 1: {
                title.text = "1st Citation";
                sprite.sortingOrder = -6;
                canvas.sortingOrder = -5;
                break;
            }
            case 2: {
                title.text = "2nd Citation";
                sprite.sortingOrder = -4;
                canvas.sortingOrder = -3;
                break;
            }
            case 3: {
                title.text = "3rd Citation";
                sprite.sortingOrder = -2;
                canvas.sortingOrder = -1;
                break;
            }
        }

        body.text = citationText;
        transform.position = enterPosition + new Vector2(0f, -18f);
    }

    private new void Update() {
        base.Update();
        
        if (Holding) {
            _entering = false;
            collider.enabled = true;
        }
        
        if (_entering) {
            rb.MovePosition(Vector2.Lerp(transform.position, enterPosition, 0.1f));
            if(Vector2.Distance(transform.position, enterPosition) < 0.1f) {
                _entering = false;
                collider.enabled = true;
            }
        }
    }
}
