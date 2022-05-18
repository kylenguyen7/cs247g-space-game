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
    [SerializeField] private GameObject introScreenParent;
    [SerializeField] private GameObject introScreenPrefab;

    [SerializeField] private GameObject bail;
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
        yield return new WaitForSecondsRealtime(2f);

        int totalAmount = 0;
        totalAmount += AddReceiptItem("Savings", PlayerData.Instance.PlayerMoney);
        yield return new WaitForSecondsRealtime(0.75f);
        
        totalAmount += AddReceiptItem($"Deliveries ({PlayerData.Instance.PackagesDeliveredToday})",
            PlayerData.Instance.PackagesDeliveredToday * PlayerData.MONEY_PER_PACKAGE);
        yield return new WaitForSecondsRealtime(0.75f);

        if (PlayerData.Instance.CitationsToday == 1) {
            totalAmount += AddReceiptItem("Citation (1)", -5);
            yield return new WaitForSecondsRealtime(0.75f);
        } else if (PlayerData.Instance.CitationsToday == 2) {
            totalAmount += AddReceiptItem("Citations (2)", -10);
            yield return new WaitForSecondsRealtime(0.75f);
        }
        
        totalAmount += AddReceiptItem("Fuel", -20);
        yield return new WaitForSecondsRealtime(0.75f);

        AddReceiptItem("Total", totalAmount);
        yield return new WaitForSecondsRealtime(0.75f);
        
        bail.SetActive(true);
        yield return new WaitForSecondsRealtime(0.75f);
        
        startNextDay.SetActive(true);
    }

    private int AddReceiptItem(String title, int amount) {
        var receiptItem = Instantiate(receiptItemPrefab, receiptItemParent.transform)
            .GetComponent<ReceiptItemController>();
        receiptItem.Init(title, amount);

        return amount;
    }

    public void OnNextDayButtonClick() {
        animator.SetTrigger("reset");
    }

    public void CreateIntroScreen() {
        Instantiate(introScreenPrefab, introScreenParent.transform);
    }
}
