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
    [SerializeField] private AudioClip onCitationDestroyedSfx;
    [SerializeField] private DialogueItem onCitationDestroyedText;

    private bool _entering = true;
    
    private RegionController _currentRegion;
    private RegionController CurrentRegion {
        get => _currentRegion;
        set {
            // Current region is the same as last frame
            if (_currentRegion == value) {
                return;
            }
            
            // Current region has changed; exit previous region
            if (_currentRegion != null) {
                _currentRegion.OnRegionExit();
            }
            
            // Enter new region, if not null
            _currentRegion = value;
            if (_currentRegion != null) {
                _currentRegion.OnRegionEnter();
            }
        }
    }
    
    private new void OnMouseUp() {
        base.OnMouseUp();
        OnDrop(CurrentRegion);
        CurrentRegion = null;
    }
    
    private void OnDrop(RegionController region) {
        if (region != null && region.RegionId == "destroy") {
            GlobalAudio.Source.PlayOneShot(onCitationDestroyedSfx);
            TextboxManager.Instance.QueueText(onCitationDestroyedText);
            Destroy(gameObject);
        }
    }

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
            
            var mousePos = GetMousePosWorldCoordinates();
            var hit = Physics2D.Raycast(
                new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z),
                Vector3.forward,
                100f,
                LayerMask.GetMask("Regions"));

            if (hit.collider != null) {
                CurrentRegion = hit.collider.GetComponent<RegionController>();
            } else {
                CurrentRegion = null;
            }
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
