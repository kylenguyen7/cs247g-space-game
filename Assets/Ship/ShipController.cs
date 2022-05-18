using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float angularVelocity;

    private void Update() {
        rb.angularVelocity = Input.GetAxisRaw("Horizontal") * angularVelocity;
        rb.AddForce(transform.right * acceleration * Input.GetAxisRaw("Vertical"));
    }
}
