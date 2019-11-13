using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI textMesh = null;

    [SerializeField] private CollectibleData collectibleData = null;

    private void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();

        collectibleData.OnGainCollectible += UpdateCurrentScore;
        UpdateCurrentScore();
    }

    private void UpdateCurrentScore() {
        textMesh.text = $"{collectibleData.CurrentCollectibleCount} / {collectibleData.CollectedWinValue}";
    }
}
