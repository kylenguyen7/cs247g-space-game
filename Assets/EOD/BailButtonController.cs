using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BailButtonController : MonoBehaviour {
    [SerializeField] private String receiptItemName;
    [SerializeField] private int cost;
    [SerializeField] private BailManager.FAMILY_MEMBER familyMember;
    [SerializeField] private GameObject bailButton;
    [SerializeField] private GameObject bailedMessage;
    [SerializeField] private AudioClip errorSfx;
    [SerializeField] private AudioClip bailSfx;
    [SerializeField] private Image familyMemberImage;
    [SerializeField] private Sprite familyMemberSprite;

    
    private void DisableBailButton() {
        bailButton.SetActive(false);
        bailedMessage.SetActive(true);
    }

    public void OnBailButtonClicked() {
        if (DayEndController.Instance.TryAddReceiptItem(receiptItemName, cost)) {
            GlobalAudio.Source.PlayOneShot(bailSfx);
            DisableBailButton();
            BailManager.Instance.BailOut(familyMember);
            familyMemberImage.sprite = familyMemberSprite;
        } else {
            GlobalAudio.Source.PlayOneShot(errorSfx);
        }
    }
}
