using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 bounds;
    [SerializeField, Range(0f, 1f)] private float followSpeed;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, bounds);
    }

    private void Update() {
        var targetPosition = ShipController.Instance.GetCameraFollowPosition();
        var newPosition = Vector2.Lerp(transform.position, targetPosition, followSpeed);
        newPosition.x = Mathf.Clamp(newPosition.x, center.x - bounds.x / 2f, center.x + bounds.x / 2f);
        newPosition.y = Mathf.Clamp(newPosition.y, center.y - bounds.y / 2f, center.y + bounds.y / 2f);

        transform.position = new Vector3(newPosition.x, newPosition.y, -10f);
    }
}
