using UnityEngine;
using TMPro;
public class InteractPrompt : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    private bool isPlayerInRange = false;

    void Start()
    {
        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Example action
            Debug.Log("Interacted!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            promptText.gameObject.SetActive(false);
        }
    }
}