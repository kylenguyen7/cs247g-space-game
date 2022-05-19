
using UnityEngine;
using System;

public class PackageController : Draggable {
    [SerializeField] private DialogueItem onPackageDeliveredText;
    [SerializeField] private DialogueItem onPackageDestroyedText;
    [SerializeField, Range(0f, 1f)] private float enterSpeed;
    [SerializeField] private AudioClip onPackageDestroyedSfx;
    [SerializeField] private AudioClip onPackageDeliveredSfx;
    [SerializeField] private String flag;
    [SerializeField] private bool shouldBeDelivered;
    
    // Entering
    [SerializeField] private Collider2D collider;
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
        if (region != null) {
            if (region.RegionId == "deliver") {
                GlobalAudio.Source.PlayOneShot(onPackageDeliveredSfx);
                
                if (flag == "decisionOne") {
                    PackageData.Instance.decisionOne = true;
                }

                if (!shouldBeDelivered) {
                    CitationsManager.Instance.CreateCitation(true);
                }
                
                
                TextboxManager.Instance.CreateText(onPackageDestroyedText);
                
                Destroy(gameObject);
            } else if (region.RegionId == "destroy") {
                GlobalAudio.Source.PlayOneShot(onPackageDestroyedSfx);
                
                if (flag == "decisionOne") {
                    PackageData.Instance.decisionOne = false;
                }
                
                if (shouldBeDelivered) {
                    CitationsManager.Instance.CreateCitation(false);
                }
                
                
                TextboxManager.Instance.CreateText(onPackageDestroyedText);
                
                Destroy(gameObject);
            }
        }
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
            rb.MovePosition(Vector2.Lerp(transform.position, Vector2.zero, enterSpeed));
            if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f) {
                _entering = false;
                collider.enabled = true;
            }
        }
    }
}