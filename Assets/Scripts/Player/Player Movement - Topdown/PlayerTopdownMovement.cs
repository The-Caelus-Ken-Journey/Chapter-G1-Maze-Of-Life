using UnityEngine;

public class PlayerTopdownMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Speed of the player movement
    [SerializeField] private float sprintSpeed = 10f; // Speed of the player when sprinting
    private Rigidbody playerRigidbody; // Reference to the player's Rigidbody component 
    private float verticalInput;
    private float horizontalInput;

    // Reference to the main camera
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player GameObject
        mainCamera = Camera.main; // Get the main camera
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        HandleMovement(); // Call the HandleMovement method to process player movement
    }

    void HandleMovement()
    {
        // Get the input from the player using Unity's Input System
        verticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)
        horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)

        // Create a new Vector3 for movement direction based on input
        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized; // Normalize to prevent faster diagonal movement

        if (inputDirection.magnitude >= 0.1f) // Check if there is any input
        {
            // Calculate the camera's forward and right vectors
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;

            // Flatten the vectors to ignore the y-axis
            cameraForward.y = 0;
            cameraRight.y = 0;

            // Normalize the vectors
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate the desired movement direction relative to the camera
            Vector3 desiredMoveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            // Determine the current speed based on whether the player is sprinting
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

            // Move the player by adding force to the Rigidbody component
            playerRigidbody.MovePosition(playerRigidbody.position + desiredMoveDirection * currentSpeed * Time.fixedDeltaTime);

            // Rotate the player based on movement direction
            Quaternion toRotation = Quaternion.LookRotation(desiredMoveDirection, Vector3.up); // Create a rotation based on movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime); // Smoothly rotate towards the movement direction
        }
    }
}
