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
    [SerializeField] private GameObject parentCanvas;
    [SerializeField] private GameObject introScreenPrefab;
    [SerializeField] private GameObject restartScreenPrefab;

    [SerializeField] private GameObject bail;
    [SerializeField] private GameObject startNextDay;

    private GameObject _totalReceiptItem;
    private int _newPlayerSavings;
    public int NewPlayerSavings => _newPlayerSavings;
    
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

        _newPlayerSavings = 0;
        _newPlayerSavings += AddReceiptItem("Savings", PlayerData.Instance.PlayerMoney, false);
        yield return new WaitForSecondsRealtime(0.75f);
        
        _newPlayerSavings += AddReceiptItem($"Salary", 60, false);
        yield return new WaitForSecondsRealtime(0.75f);

        if (CitationsManager.Instance.NumCitations == 1) {
            _newPlayerSavings += AddReceiptItem("Citation (1)", -20, false);
            yield return new WaitForSecondsRealtime(0.75f);
        } else if (CitationsManager.Instance.NumCitations == 2) {
            _newPlayerSavings += AddReceiptItem("Citations (2)", -40, false);
            yield return new WaitForSecondsRealtime(0.75f);
        }
        
        _newPlayerSavings += AddReceiptItem("Fuel", -20, false);
        yield return new WaitForSecondsRealtime(0.75f);

        AddReceiptItem("Total", _newPlayerSavings, true);
        yield return new WaitForSecondsRealtime(0.75f);
        
        bail.SetActive(true);
        yield return new WaitForSecondsRealtime(0.75f);
        
        startNextDay.SetActive(true);
    }

    public bool TryAddReceiptItem(String name, int cost) {
        if (_newPlayerSavings + cost < 0) {
            return false;
        }
        
        Destroy(_totalReceiptItem);
        _newPlayerSavings += AddReceiptItem(name, cost, false);
        AddReceiptItem("Total", _newPlayerSavings, true);
        return true;
    }

    private int AddReceiptItem(String title, int amount, bool total) {
        var receiptItem = Instantiate(receiptItemPrefab, receiptItemParent.transform)
            .GetComponent<ReceiptItemController>();
        receiptItem.Init(title, amount, total);

        if (total) {
            _totalReceiptItem = receiptItem.gameObject;
        }

        return amount;
    }

    public void OnNextDayButtonClick() {
        animator.SetTrigger("reset");
    }

    public void SetDataForNextDay() {
        PlayerData.Instance.AdvanceDay();
        PackageData.Instance.AdvanceDay();
        
        foreach (CitationController citation in FindObjectsOfType<CitationController>()) {
            Destroy(citation.gameObject);
        }
        
        foreach (PackageController package in FindObjectsOfType<PackageController>()) {
            Destroy(package.gameObject);
        }
        
        for (int i = 0; i < receiptItemParent.transform.childCount; i++) {
            Destroy(receiptItemParent.transform.GetChild(i).gameObject);
        }

        PlayerData.Instance.PlayerMoney = _newPlayerSavings;
    }

    public void CreateIntroScreen() {
        Instantiate(introScreenPrefab, parentCanvas.transform);
    }

    public void CreateRestartScreen() {
        Instantiate(restartScreenPrefab, parentCanvas.transform);
    }
}
