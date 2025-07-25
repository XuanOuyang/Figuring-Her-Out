using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private int playersInRange = 0;

    void Update()
    {
        if (playersInRange > 0 && Input.GetKeyDown(KeyCode.W))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInRange++;
            Debug.Log("Player entered the level loader zone.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInRange = Mathf.Max(0, playersInRange - 1);
            Debug.Log("Player left the level loader zone.");
        }
    }
}