using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    public static ShipController Instance;
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float angularVelocity;
    [SerializeField] private AudioSource shipAudio;
    public bool HasMoved { get; private set; }

    private Transform currentPlanetTransform;
    private Planet currentPlanet;
    public Planet CurrentPlanet => currentPlanet;

    private void Update() {
        rb.angularVelocity = Input.GetAxisRaw("Horizontal") * angularVelocity;

        float verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput != 0f && !shipAudio.isPlaying) {
            shipAudio.UnPause();
        } else if(verticalInput == 0f && shipAudio.isPlaying) {
            shipAudio.Pause();
        }

        if (!HasMoved && verticalInput != 0f) {
            HasMoved = true;
        }
        
        rb.AddForce(transform.right * acceleration * verticalInput * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlanetRegion")) {
            currentPlanetTransform = other.transform;
            currentPlanet = other.gameObject.GetComponent<PlanetRegionController>().Planet;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        currentPlanet = Planet.NONE;
    }

    public Vector2 GetCameraFollowPosition() {
        if (currentPlanet != Planet.NONE) {
            return currentPlanetTransform.position;
        }

        return transform.position;
    }
}
