using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScreenController : MonoBehaviour
{
    // Called at end of fade in animation; just create Restart Screen prefab
    public void RestartDay() {
        PackageData.Instance.RestartDay();
        PlayerData.Instance.RestartDay();
        DayEndController.Instance.CreateIntroScreen();

        foreach (CitationController citation in FindObjectsOfType<CitationController>()) {
            Destroy(citation.gameObject);
        }
        
        foreach (PackageController package in FindObjectsOfType<PackageController>()) {
            Destroy(package.gameObject);
        }
        
        Destroy(gameObject);
    }
}
