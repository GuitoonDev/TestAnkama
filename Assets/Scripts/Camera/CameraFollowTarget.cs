using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform followTarget = null;
    [SerializeField] private float offsetX = 0f;

    private void OnValidate() {
        FollowTarget();
    }

    private void LateUpdate() {
        FollowTarget();
    }

    private void FollowTarget() {
        if (followTarget != null) {
            Vector3 newPosition = transform.position;
            newPosition.x = followTarget.position.x + offsetX;
            transform.position = newPosition;
        }
    }
}
