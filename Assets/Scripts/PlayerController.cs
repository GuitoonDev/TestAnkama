using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public UnityAction OnGameLoose;

    // Animator Parameters
    private static readonly int animVelocityY = Animator.StringToHash("velocityY");
    private static readonly int animIsSliding = Animator.StringToHash("isSliding");

    private new Rigidbody2D rigidbody;
    private Animator animator;

    [SerializeField, EnumFlag("Player Bonus Enum")] private PlayerBonus playerBonusEnum = default(PlayerBonus);

    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;

    [Header("Jump Detection")]
    [SerializeField] private LayerMask groundLayer = default(LayerMask);
    [SerializeField] private Vector2 groundDetectionPoint = Vector2.zero;
    [SerializeField] private float groundDetectionRadius = 0.25f;

    [Header("Bonus Parameters")]
    [SerializeField] private float superJumpForce = 1f;
    [SerializeField] private SpriteRenderer InvincibilitySprite = null;

    [Header("Collectible Datas")]
    [SerializeField] private CollectibleData collectibleData = null;

    private bool isJumping;
    private bool isSliding;
    private bool isStopMoving;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2) transform.position + groundDetectionPoint, groundDetectionRadius);
        Gizmos.color = Color.white;
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        collectibleData.OnGameWon += OnGameWonCallback;
    }

    private void OnDestroy() {
        OnGameLoose();
    }

    private void Update() {
        if (playerBonusEnum.HasFlag(PlayerBonus.Invincibility) != InvincibilitySprite.gameObject.activeSelf) {
            InvincibilitySprite.gameObject.SetActive(playerBonusEnum.HasFlag(PlayerBonus.Invincibility));
        }
    }

    private void FixedUpdate() {
        Vector2 newVelocity;
        if (isStopMoving) {
            newVelocity = Vector2.zero;
        }
        else {
            newVelocity = new Vector2(speed * Time.fixedDeltaTime, rigidbody.velocity.y);

            bool isGroundFound = Physics2D.OverlapCircle((Vector2) transform.position + groundDetectionPoint, groundDetectionRadius, groundLayer);
            if (!isJumping && !isGroundFound) {
                isJumping = true;
                newVelocity.x = jumpSpeed * Time.fixedDeltaTime;

                if (playerBonusEnum.HasFlag(PlayerBonus.SuperJump)) {
                    newVelocity.y = superJumpForce * Time.fixedDeltaTime;
                }
                else {
                    newVelocity.y = jumpForce * Time.fixedDeltaTime;
                }
            }
            else if (isJumping && isGroundFound) {
                isJumping = false;
            }

            animator.SetFloat(animVelocityY, newVelocity.y);

        }

        rigidbody.velocity = newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Obstacle"))) {
            if (playerBonusEnum.HasFlag(PlayerBonus.Invincibility)) {
                Destroy(other.gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerBonus"))) {
            Bonus collideBonus = other.gameObject.GetComponent<Bonus>();
            if (collideBonus != null) {
                StartCoroutine(StartBonusEffectCoroutine(collideBonus));
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Collectible"))) {
            collectibleData.IncrementCollectibleGain();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator StartBonusEffectCoroutine(Bonus collideBonus) {
        playerBonusEnum |= collideBonus.TargetBonus;

        yield return new WaitForSeconds(collideBonus.TimeDuration);

        playerBonusEnum &= ~collideBonus.TargetBonus;
    }

    private void OnGameWonCallback() {
        isStopMoving = true;
    }
}
