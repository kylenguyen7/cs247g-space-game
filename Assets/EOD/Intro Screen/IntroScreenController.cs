using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroScreenController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;

    private void Start() {
        PlayerData.Instance.AdvanceDay();
        PackageData.Instance.AdvanceDay();
        text.text = $"DAY {PlayerData.Instance.CurrentDay}";
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
