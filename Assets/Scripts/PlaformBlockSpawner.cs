using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformBlockSpawner : MonoBehaviour
{
    [SerializeField] private SpawnablePlatformBlockData spawnablePlatformBlockData = null;

    private void Start() {
        spawnablePlatformBlockData.OnPlatformTriggered += SpawnNewPlatform;
    }

    private void SpawnNewPlatform() {
        Instantiate(spawnablePlatformBlockData.PickOnePlatformBlock(), transform.position, Quaternion.identity);
    }
}
