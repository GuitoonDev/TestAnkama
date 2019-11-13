using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private CollectibleData collectibleData = null;

    private void Start() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = collectibleData.PickOneCollectibleSprite();
    }
}
