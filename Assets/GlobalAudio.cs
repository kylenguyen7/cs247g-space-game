using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour {
    public static GlobalAudio Instance;
    [SerializeField] private AudioSource source;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public static AudioSource Source => Instance.source;
}
