using System.Collections;
using UnityEngine;

public class RulesButtonController : ButtonController {
    [SerializeField] private GameObject rules;
    private bool _clicked;

    private void Awake() {
        StartCoroutine(Flash());
    }

    protected override void OnButtonPressed() {
        _clicked = true;
        PackageButtonController.Instance.SetButtonActive(false);
        rules.SetActive(true);
    }

    private IEnumerator Flash() {
        while (!_clicked) {
            ToggleOutline();
            yield return new WaitForSecondsRealtime(1f);
        }

        ToggleOutline(false);
    }
}
