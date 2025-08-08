using System.Collections;
using UnityEngine;

public class TriggerActionMover : MonoBehaviour
{
    public GameObject player;              // Reference to player
    public MonoBehaviour playerMovement;  // Reference to player's movement script
    public GameObject appearObject;        // The object to show/hide
    public Transform moveTarget;           // The location to move to
    public int firstFacce = 1;
    public int secondFacce = 1;
    public float delayTime = 0.5f;         // Time to stay flipped
    public float moveSpeed = 3f;           // Speed of movement toward target

    private bool triggered = false;        // Prevents re-triggering

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.gameObject == player)
        {
            triggered = true;
            StartCoroutine(PerformActions());
        }
    }

    IEnumerator PerformActions()
    {
        // Disable player movement
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Flip X scale to -1
        Vector3 originalScale = transform.localScale;
        transform.localScale = new Vector3(firstFacce * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        yield return new WaitForSeconds(delayTime);

        // Show object
        if (appearObject != null)
            appearObject.SetActive(true);

        yield return new WaitForSeconds(0.4f); // Short show time

        // Hide object
        if (appearObject != null)
            appearObject.SetActive(false);

        // Flip back
        transform.localScale = new Vector3(secondFacce * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Move smoothly to target position
        if (moveTarget != null)
        {
            while (Vector3.Distance(transform.position, moveTarget.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
                yield return null; // wait for next frame
            }
            transform.position = moveTarget.position; // Snap exactly to target
        }

        // Re-enable player movement
        if (playerMovement != null)
            playerMovement.enabled = true;
    }
}
