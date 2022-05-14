using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PackageData : MonoBehaviour {
    public bool decisionOne;
    public bool decisionTwo;
    public static PackageData Instance;
    [System.Serializable] 
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
            List<PackagePair> tempPackages = currPackages;
            if (tempPackages == dayOnePackages) {
                currPackages = dayTwoPackages;
                _currentPackageIndex = 0;
            } if (tempPackages == dayTwoPackages && decisionOne) {
                currPackages = dayThreeAPackages;
                _currentPackageIndex = 0;
            } if (tempPackages == dayTwoPackages && !decisionOne) {
                currPackages = dayThreeBPackages;
                _currentPackageIndex = 0;
            } else {
                return null;
            }
        }
        GameObject result;
        if (currPackages[_currentPackageIndex].flag == "") {
            
            result = currPackages[_currentPackageIndex].a;
            _currentPackageIndex++;
            return result;

        } else {
             if (currPackages[_currentPackageIndex].flag == "decisionOne") {
                if (decisionOne) {
                    result = currPackages[_currentPackageIndex].a;
                    _currentPackageIndex++;
                    return result;
                } else {
                    result= currPackages[_currentPackageIndex].b;
                    _currentPackageIndex++;
                    return result;
                 }
             }
                if (currPackages[_currentPackageIndex].flag == "decisionTwo") {
                    if (decisionTwo) {
                        result =  currPackages[_currentPackageIndex].a;
                        _currentPackageIndex++;
                        return result;
                    } else {
                        result = currPackages[_currentPackageIndex].b;
                        _currentPackageIndex++;
                        return result;
                    }
                }

        }
        return null;
        // GameObject result = dayOnePackages[_currentPackageIndex];
        // _currentPackageIndex++;
        // return result;
    }
}
