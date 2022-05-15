using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PackageData : MonoBehaviour {
    public bool decisionOne;
    public bool decisionTwo;
    public static PackageData Instance;
    
    [Serializable] 
    public struct PackagePair{
        public GameObject a;
        public GameObject b;
        public String flag;
    }    
    private List<PackagePair> currPackages;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currPackages = dayOnePackages;
    }

    [SerializeField] private List<PackagePair> dayOnePackages; 
    [SerializeField] private List<PackagePair> dayTwoPackages; 
    [SerializeField] private List<PackagePair> dayThreeAPackages;
    [SerializeField] private List<PackagePair> dayThreeBPackages;
    private int _currentPackageIndex;

    public GameObject GetNextPackage() {
        if (_currentPackageIndex == currPackages.Count) {
            _currentPackageIndex = 0;
            if (currPackages == dayOnePackages) {
                currPackages = dayTwoPackages;
            } else if (currPackages == dayTwoPackages) {
                currPackages = decisionOne ? dayThreeAPackages : dayThreeBPackages;
            } else {
                return null;
            }
        }
        
        GameObject result = CalculateNextPackage();
        _currentPackageIndex++;
        return result;
    }

    private GameObject CalculateNextPackage() {
        if (currPackages[_currentPackageIndex].flag == "decisionOne") {
            return decisionOne ? currPackages[_currentPackageIndex].a : currPackages[_currentPackageIndex].b;
        }
        
        if (currPackages[_currentPackageIndex].flag == "decisionTwo") {
            return decisionTwo ? currPackages[_currentPackageIndex].a : currPackages[_currentPackageIndex].b;
        }
        
        return currPackages[_currentPackageIndex].a;
    }
}
