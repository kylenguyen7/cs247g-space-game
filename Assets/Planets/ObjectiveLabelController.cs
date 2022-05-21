using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveLabelController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI tmp;

    public void Update() {
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
    }
}
