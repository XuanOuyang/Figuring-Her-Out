using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
    public Transform spawnPoint;
    public float respawnDelay = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[KillZone] Trigger entered by: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("[KillZone] Player entered the kill zone. Starting respawn coroutine.");
            StartCoroutine(RespawnPlayer(other.gameObject));
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        Debug.Log("[KillZone] Respawn coroutine started.");

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            Debug.Log("[KillZone] Player Rigidbody set to Static (frozen).");
        }

        yield return new WaitForSeconds(respawnDelay);

        player.transform.position = spawnPoint.position;
        Debug.Log($"[KillZone] Player respawned at: {spawnPoint.position}");

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("[KillZone] Player Rigidbody set back to Dynamic.");
        }

        Debug.Log("[KillZone] Respawn complete.");
    }
}
