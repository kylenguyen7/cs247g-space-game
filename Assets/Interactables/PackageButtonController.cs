using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PackageButtonController : ButtonController {
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip onPackageEnterSfx;

    public static PackageButtonController Instance;
    private Vector2 _startingPosition;
    private Vector2 _targetPosition;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        
        _startingPosition = transform.position;
        Instance = this;
    }

    private bool Active {
        set {
            if (value) {
                collider.enabled = true;
                _targetPosition = _startingPosition;
            } else {
                collider.enabled = false;
                _targetPosition = _startingPosition + new Vector2(0f, 7f);
            }
        }
    }
    
    protected override void OnButtonPressed() {
        if (FindObjectOfType<PackageController>() == null) {
            GameObject nextPackage = PackageData.Instance.GetNextPackage();
            if (nextPackage == null) return;
            
            Instantiate(nextPackage, new Vector2(0f, -18f), Quaternion.identity);
            GlobalAudio.Source.PlayOneShot(onPackageEnterSfx);
        }
    }

    private new void Update() {
        base.Update();
        
        rb.MovePosition(Vector2.Lerp(transform.position, _targetPosition, 0.1f));

        var origin = PackageData.Instance.GetNextPackageOrigin();
        if (FindObjectOfType<PackageController>() == null && origin != Planet.NONE && origin == ShipController.Instance.CurrentPlanet) {
            Active = true;
        } else {
            Active = false;
        }
    }

    public void SetButtonActive(bool active) {
        _active = active;
    }
}
