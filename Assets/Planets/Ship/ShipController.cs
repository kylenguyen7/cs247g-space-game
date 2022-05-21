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

    private Transform currentPlanetTransform;
    private Planet currentPlanet;
    public Planet CurrentPlanet => currentPlanet;

    private void Update() {
        rb.angularVelocity = Input.GetAxisRaw("Horizontal") * angularVelocity * Time.deltaTime;
        rb.AddForce(transform.right * acceleration * Input.GetAxisRaw("Vertical") * Time.deltaTime);
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
