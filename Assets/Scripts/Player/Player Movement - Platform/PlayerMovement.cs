using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    private Rigidbody playerRigidbody;
    private float horizontalInput;
    public bool isFacingRight = true; // Track if the player is facing right

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        isFacingRight = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(horizontalInput * speed, playerRigidbody.linearVelocity.y, 0);
        playerRigidbody.linearVelocity = movement;

        // Rotate the player to face the direction of movement
        if (horizontalInput != 0)
        {
            // Update isFacingRight based on horizontal input
            isFacingRight = horizontalInput > 0;

            // Rotate the player to face the direction of movement
            Quaternion targetRotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}