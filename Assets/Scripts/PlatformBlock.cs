using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBlock : MonoBehaviour
{
    [SerializeField] private SpawnablePlatformBlockData spawnablePlatformBlockData = null;
    [SerializeField] private float spawnPlatformThresholdX = -20f;

    private Camera mainCamera;

    private bool isTriggerSend;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Update() {
        float relativeHorizontalPositionToCamera = transform.position.x - mainCamera.transform.position.x;
        if (!isTriggerSend && relativeHorizontalPositionToCamera <= spawnPlatformThresholdX) {
            isTriggerSend = true;
            spawnablePlatformBlockData.TriggerSpawnNewPlatform();
            Destroy(gameObject, 10);
        }
    }
}
