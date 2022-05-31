using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceScreenController : MonoBehaviour {
    [SerializeField] private GameObject nextSequenceScreen;
    [SerializeField] private string nextSceneName;
    [SerializeField] private Animator animator;
    [SerializeField] private bool restartScreen;
    
    public void OnMouseDown() {
        if (!restartScreen) {
            animator.SetTrigger("fade out");
        }
    }

    private void Update() {
        if (Input.anyKey && !restartScreen) {
            animator.SetTrigger("fade out");
        }

        if (restartScreen && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Title");
        }
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
