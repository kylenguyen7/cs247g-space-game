using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PackageButtonController : ButtonController {
    [SerializeField] private AudioClip onPackageEnterSfx;
    public static PackageButtonController Instance;
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    protected override void OnButtonPressed() {
        if (FindObjectOfType<PackageController>() == null) {
            GameObject nextPackage = PackageData.Instance.GetNextPackage();
            if (nextPackage == null) return;
            
            Instantiate(nextPackage, new Vector2(0f, -18f), Quaternion.identity);
            GlobalAudio.Source.PlayOneShot(onPackageEnterSfx);
        }
    }

    public void SetButtonActive(bool active) {
        _active = active;
    }
}
