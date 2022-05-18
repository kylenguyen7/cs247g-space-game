using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public static PlayerData Instance;
    public static readonly int MONEY_PER_PACKAGE = 10;

    public void Awake() {
        if (Instance != null) {
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
    }
    
    public int PlayerMoney { get; set; }
    public int PackagesDeliveredToday { get; set; }
    public int CitationsToday { get; set; }
    public int CurrentDay { get; private set; }

    public void AdvanceDay() {
        CurrentDay++;
        CitationsToday = 0;
        PackagesDeliveredToday = 0;
    }
}
