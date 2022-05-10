using System;
using UnityEngine;

public class RegionController : Interactable {
    public void OnRegionEnter() {
        ToggleOutline(true);
    }

    public void OnRegionExit() {
        ToggleOutline(false);
    }
}