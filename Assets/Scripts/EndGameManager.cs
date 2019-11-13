using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private RectTransform gameLooseScreen = null;
    [SerializeField] private RectTransform gameWonScreen = null;
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private CollectibleData collectibleData = null;

    private void Awake() {
        playerController.OnGameLoose += DisplayGameLooseScreen;
        collectibleData.OnGameWon += DisplayGameWonScreen;
    }

    private void DisplayGameLooseScreen() {
        gameLooseScreen.gameObject.SetActive(true);
    }

    private void DisplayGameWonScreen() {
        gameWonScreen.gameObject.SetActive(true);
    }
}
