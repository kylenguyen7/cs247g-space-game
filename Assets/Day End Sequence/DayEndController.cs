using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayEndController : MonoBehaviour {
    public static DayEndController Instance;
    private Coroutine _currentTransition;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject receiptItemParent;
    [SerializeField] private GameObject receiptItemPrefab;
    [SerializeField] private GameObject startNextDay;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EndDay() {
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence() {
        yield return new WaitForSecondsRealtime(1f);
        
        animator.SetTrigger("enter");
        yield return new WaitForSecondsRealtime(0.5f);
        
        var receiptItem = Instantiate(receiptItemPrefab, receiptItemParent.transform)
            .GetComponent<ReceiptItemController>();
        receiptItem.Init("Wallet", PlayerData.Instance.PlayerMoney);
        yield return new WaitForSecondsRealtime(0.5f);
        
        receiptItem = Instantiate(receiptItemPrefab, receiptItemParent.transform)
            .GetComponent<ReceiptItemController>();
        receiptItem.Init("Salary", PlayerData.Instance.SalaryToday);
        yield return new WaitForSecondsRealtime(0.5f);
        
        receiptItem = Instantiate(receiptItemPrefab, receiptItemParent.transform)
            .GetComponent<ReceiptItemController>();
        receiptItem.Init("Fuel", -20);
        yield return new WaitForSecondsRealtime(0.5f);
        
        startNextDay.SetActive(true);
        
        // Current balance
        // Salary
        // Fuel
        // Citations (sometimes)
        // Bail (up to three, sometimes)
    }
}
