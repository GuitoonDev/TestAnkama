using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Animator Parameters
    private static readonly int animVelocityY = Animator.StringToHash("velocityY");
    private static readonly int animIsSliding = Animator.StringToHash("isSliding");

    private new Rigidbody2D rigidbody;
    private Animator animator;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;

    [Header("Jump Detection")]
    [SerializeField] private LayerMask groundLayer = default(LayerMask);
    [SerializeField] private Vector2 groundDetectionPoint = Vector2.zero;
    [SerializeField] private float groundDetectionRadius = 0.25f;

    [Header("Dead Zone")]
    [SerializeField] private float thresholdPositionY = -10f;

    bool isJumping;
    bool isSliding;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2) transform.position + groundDetectionPoint, groundDetectionRadius);
        Gizmos.color = Color.white;
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (transform.position.y <= thresholdPositionY) {
            // Game over
            Destroy(gameObject);

            // Trigger Game Over event
        }
    }

    private void FixedUpdate() {
        Vector2 newVelocity = new Vector2(speed * Time.fixedDeltaTime, rigidbody.velocity.y);

        bool isGroundFound = Physics2D.OverlapCircle((Vector2) transform.position + groundDetectionPoint, groundDetectionRadius, groundLayer);
        if (!isJumping && !isGroundFound) {
            Debug.Log("Jump !");
            isJumping = true;
            newVelocity.y = jumpForce * Time.fixedDeltaTime;
        }
        else if (isJumping && isGroundFound) {
            Debug.Log("Ground falling");
            isJumping = false;
        }

        animator.SetFloat(animVelocityY, newVelocity.y);
        rigidbody.velocity = newVelocity;
    }
}
