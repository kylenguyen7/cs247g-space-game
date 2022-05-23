using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayEndTitleController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI dayX;

    public void Update() {
        dayX.text = $"DAY {PlayerData.Instance.CurrentDay}";
    }
}
