using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroScreenController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;

    private void Start() {
        text.text = $"DAY {PlayerData.Instance.CurrentDay} of 3";
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
