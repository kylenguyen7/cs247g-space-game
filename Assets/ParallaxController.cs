using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {
    [SerializeField] private Transform camera;
    [SerializeField, Range(0f, 1f)] private float parallaxFactor;

    private Vector2 cameraStartingPos;
    private Vector2 startingPos;

    private void Start() {
        startingPos = transform.position;
        cameraStartingPos = camera.position;
    }

    private void Update() {
        Vector2 pos = startingPos + ((Vector2) camera.position - cameraStartingPos) * parallaxFactor;
        transform.position = pos;
    }
}
