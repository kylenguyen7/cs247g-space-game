using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RulesButtonController : ButtonController {
    [SerializeField] private GameObject rules;
    protected override void OnButtonPressed() {
        PackageButtonController.Instance.SetButtonActive(false);
        rules.SetActive(true);
    }
}
