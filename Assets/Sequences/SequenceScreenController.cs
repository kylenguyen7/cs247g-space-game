using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceScreenController : MonoBehaviour {
    [SerializeField] private GameObject nextSequenceScreen;
    [SerializeField] private string nextSceneName;
    [SerializeField] private Animator animator;
    
    public void OnMouseDown() {
        animator.SetTrigger("fade out");
    }

    public void DestroyMe() {
        if (nextSequenceScreen != null) {
            Instantiate(nextSequenceScreen, transform.position, Quaternion.identity);
        } else if (nextSceneName != "") {
            SceneManager.LoadScene(nextSceneName);
        } else {
            Debug.LogWarning("A sequence screen was destroyed without a specified next action! Assign a next screen or scene.");
        }
        Destroy(gameObject);
    }
}
