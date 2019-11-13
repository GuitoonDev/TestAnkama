using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private CollectibleData collectibleData = null;

    private void Awake() {
        collectibleData.Reset();
    }
}
