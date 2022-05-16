using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public static PlayerData Instance;

    public void Awake() {
        if (Instance != null) {
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
    }
    
    public int PlayerMoney => 100;
    public int SalaryToday => 50;
    public int CitationsToday => 2;
}
