using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageData : MonoBehaviour {
    public static PackageData Instance;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private List<GameObject> dayOnePackages;
    private int _currentPackageIndex;

    public GameObject GetNextPackage() {
        if (_currentPackageIndex == dayOnePackages.Count) {
            return null;
        }
        
        GameObject result = dayOnePackages[_currentPackageIndex];
        _currentPackageIndex++;
        return result;
    }
}
