using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class ReceiptItemController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private Color negativeColor;
    [SerializeField] private RectTransform receiptItemTransform;
    [SerializeField] private GameObject totalLine;

    public void Init(String itemText, int itemAmount, bool total) {
        if (itemAmount < 0) {
            text.color = negativeColor;
            amount.color = negativeColor;
        }

        if (total) {
            receiptItemTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 65f);
            totalLine.SetActive(true);
        }
        
        text.text = itemText;
        amount.text = itemAmount < 0 ? $"-${-itemAmount}" : $"${itemAmount}";
    }
}