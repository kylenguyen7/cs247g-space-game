using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationLabelController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI locationLabel;

    public void Update() {
        String location = ShipController.Instance.CurrentPlanet.ToString();
        locationLabel.text = $"Current location: {location}";
    }
}
