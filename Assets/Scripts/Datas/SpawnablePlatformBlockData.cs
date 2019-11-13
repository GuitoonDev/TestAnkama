using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SpawnablePlatformBlockData", menuName = "Game/Spawnable Platform Block Data")]
public class SpawnablePlatformBlockData : ScriptableObject
{
    public UnityAction OnPlatformTriggered;

    [SerializeField] private Transform[] platformBlockArray = null;

    public Transform PickOnePlatformBlock() {
        return platformBlockArray[Random.Range(0, platformBlockArray.Length)];
    }

    public void TriggerSpawnNewPlatform() {
        OnPlatformTriggered();
    }
}
