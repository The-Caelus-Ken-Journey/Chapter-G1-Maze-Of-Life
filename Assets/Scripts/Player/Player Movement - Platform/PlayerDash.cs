using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 1f;

    private bool canDash = true;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 dashDirection = playerMovement.isFacingRight ? transform.right * horizontalInput * dashDistance : -transform.right * horizontalInput * dashDistance;

        if (horizontalInput != 0)
        {
            // Store the original position
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + dashDirection;

            float dashTime = 0.2f; // Duration of the dash
            float elapsedTime = 0f;

            while (elapsedTime < dashTime)
            {
                // Smoothly interpolate the position
                transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / dashTime));
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final position is set
            transform.position = targetPosition;
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
