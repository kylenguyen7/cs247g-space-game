using System;
using UnityEngine;

public class RegionController : Interactable {
    [SerializeField] private String regionId;
    public String RegionId => regionId;
    
    public void OnRegionEnter() {
        ToggleOutline(true);
    }

    public void OnRegionExit() {
        ToggleOutline(false);
    }
}