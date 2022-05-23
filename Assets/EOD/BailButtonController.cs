using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BailButtonController : MonoBehaviour {
    [SerializeField] private String receiptItemName;
    [SerializeField] private int cost;
    [SerializeField] private BailManager.FAMILY_MEMBER familyMember;
    [SerializeField] private GameObject bailButton;
    [SerializeField] private GameObject bailedMessage;
    [SerializeField] private AudioClip errorSfx;
    
    private void DisableBailButton() {
        bailButton.SetActive(false);
        bailedMessage.SetActive(true);
    }

    public void OnBailButtonClicked() {
        if (DayEndController.Instance.TryAddReceiptItem(receiptItemName, cost)) {
            DisableBailButton();
            BailManager.Instance.BailOut(familyMember);
        } else {
            GlobalAudio.Source.PlayOneShot(errorSfx);
        }
    }
}
