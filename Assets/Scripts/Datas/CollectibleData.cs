using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CollectibleData", menuName = "Game/Collectible Data")]
public class CollectibleData : ScriptableObject
{
    public UnityAction OnGainCollectible;
    public UnityAction OnGameWon;

    [SerializeField] private int collectedWinValue = 3;
    [SerializeField] private Sprite[] spriteArray = null;

    public int CollectedWinValue => collectedWinValue;
    public int CurrentCollectibleCount => currentCollectibleCount;

    private int currentCollectibleCount;

    public void Reset() {
        currentCollectibleCount = 0;
    }

    public void IncrementCollectibleGain() {
        currentCollectibleCount++;

        OnGainCollectible();

        if (currentCollectibleCount >= collectedWinValue) {
            OnGameWon();
        }
    }

    public Sprite PickOneCollectibleSprite() {
        return spriteArray[Random.Range(0, spriteArray.Length)];
    }
}
