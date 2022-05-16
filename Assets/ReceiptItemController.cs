using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceiptItemController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private Color negativeColor;

    public void Init(String itemText, int itemAmount) {
        if (itemAmount < 0) {
            text.color = negativeColor;
            amount.color = negativeColor;
        }
        
        text.text = itemText;
        amount.text = itemAmount < 0 ? $"-${itemAmount}" : $"${itemAmount}";
    }
}
