using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Diagnostics;

public class ObjectiveLabelController : MonoBehaviour {
    public static ObjectiveLabelController Instance;
    
    [SerializeField] private TextMeshProUGUI tmp;
    private bool _messageOverriden;
    private Coroutine flashMessageCoroutine;
    private float _lifetime;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Update() {
        if (_lifetime > 5f && !ShipController.Instance.HasMoved) {
            tmp.text = "Use WASD or Arrow Keys to move!"; 
            return;
        }
        
        if (_messageOverriden) return;
    
        var currentPackage = FindObjectOfType<PackageController>();
        if (currentPackage == null) {
            Planet nextOrigin = PackageData.Instance.GetNextPackageOrigin();
            if (nextOrigin != Planet.NONE) {
                tmp.text = $"Pick up next package from {nextOrigin.ToString()}";
            } else {
                tmp.text = "";
            }
        }
        else {
            Planet origin = currentPackage.OriginPlanet;
            Planet dest = currentPackage.DestPlanet;
            
            tmp.text = $"Current package - From: {origin.ToString()} / To: {dest.ToString()}";
        }

        _lifetime += Time.deltaTime;
    }

    public void FlashMessage(String message, float duration, Color color) {
        if (flashMessageCoroutine != null) {
            StopCoroutine(flashMessageCoroutine);
        }

        flashMessageCoroutine = StartCoroutine(FlashMessageSequence(message, duration, color));
    }

    private IEnumerator FlashMessageSequence(string message, float duration, Color color) {
        tmp.text = message;
        tmp.color = color;
        _messageOverriden = true;
        yield return new WaitForSecondsRealtime(duration);

        tmp.color = Color.white;
        _messageOverriden = false;
        flashMessageCoroutine = null;
    }
}
