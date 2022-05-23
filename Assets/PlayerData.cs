using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public static PlayerData Instance;
    public static readonly int MONEY_PER_PACKAGE = 10;
    [SerializeField] private int initialPlayerMoney;

    public void Awake() {
        if (Instance != null) {
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
        CurrentDay = 1;
        PlayerMoney = initialPlayerMoney;
    }
    
    public int PlayerMoney { get; set; }
    public int PackagesDeliveredToday { get; set; }
    public int CurrentDay { get; private set; }

    public void AdvanceDay() {
        CurrentDay++;
        CitationsManager.Instance.NumCitations = 0;
        PackagesDeliveredToday = 0;
    }

    public void RestartDay() {
        CitationsManager.Instance.NumCitations = 0;
        PackagesDeliveredToday = 0;
    }
}
