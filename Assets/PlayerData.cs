using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (CurrentDay == 3) {
            bool one = PackageData.Instance.decisionOne;
            bool two = PackageData.Instance.decisionTwo;

            if (one && two) {
                SceneManager.LoadScene("Ending AA");
            } else if (one && !two) {
                SceneManager.LoadScene("Ending AB");
            } else if (!one && two) {
                SceneManager.LoadScene("Ending BA");
            } else {
                SceneManager.LoadScene("Ending BB");
            }
            return;
        }
        
        
        CurrentDay++;
        CitationsManager.Instance.NumCitations = 0;
        PackagesDeliveredToday = 0;
    }

    public void RestartDay() {
        CitationsManager.Instance.NumCitations = 0;
        PackagesDeliveredToday = 0;
    }
}
