// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
//
// public class DayEndController : MonoBehaviour {
//     public static DayEndController Instance;
//     [SerializeField] private Image blackScreen;
//     [SerializeField] private float fadeTime;
//     [SerializeField] private float secondsPerFadeTick;
//
//     private Coroutine _currentTransition;
//     
//     private void Awake() {
//         if (Instance != null) {
//             Destroy(gameObject);
//             return;
//         }
//
//         Instance = this;
//         
//         if (!blackScreen.gameObject.activeSelf) {
//             blackScreen.gameObject.SetActive(true);
//         }
//
//         if (!blackScreen.enabled) {
//             blackScreen.enabled = true;
//         }
//         
//         Color col = blackScreen.color;
//         col.a = 0;
//         blackScreen.color = col;
//     }
//
//     public void SaveSceneAndLoadNewScene(String sceneName, Vector2 playerPosition, Vector2 playerDirection) {
//         if (_currentTransition != null) {
//             Debug.LogError($"Cannot transition to {sceneName} while another transition is occurring!");
//             return;
//         }
//
//         _playerTransportPosition = playerPosition;
//         _playerTransportDirection = playerDirection;
//         
//         _currentTransition = StartCoroutine(TransitionSequence(sceneName));
//     }
//
//     private IEnumerator TransitionSequence(String sceneName) {
//         Time.timeScale = 0f;
//         foreach (var saveable in FindObjectsOfType<Saveable>()) {
//             saveable.Save();
//         }
//
//         Color col = blackScreen.color;
//         for (float alpha = 0; alpha < 1f; alpha += secondsPerFadeTick / fadeTime) {
//             col.a = alpha;
//             blackScreen.color = col;
//             yield return new WaitForSecondsRealtime(secondsPerFadeTick);
//         }
//         col.a = 1;
//         blackScreen.color = col;
//
//         SceneManager.sceneLoaded += OnSceneLoaded;
//         SceneManager.LoadScene(sceneName);
//         
//         yield return new WaitForSecondsRealtime(0.5f);
//         for (float alpha = 1; alpha > 0; alpha -= secondsPerFadeTick / fadeTime) {
//             col.a = alpha;
//             blackScreen.color = col;
//             yield return new WaitForSecondsRealtime(secondsPerFadeTick);
//         }
//         
//         col.a = 0;
//         blackScreen.color = col;
//         Time.timeScale = 1;
//         _currentTransition = null;
//     }
//
//     void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
//         FindObjectOfType<PlayerController>().Teleport(_playerTransportPosition, _playerTransportDirection);
//         FindObjectOfType<CameraController>().SetPositionWithinBounds(_playerTransportPosition);
//         
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }
// }
