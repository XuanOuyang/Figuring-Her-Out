using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    public KeyCode switchKey = KeyCode.E;
    public float SeeThoughtAmount = 0.01f;

    private GameObject[] redPlatforms;
    private GameObject[] bluePlatforms;
    private bool redActive = true;

    void Start()
    {
        redPlatforms = GameObject.FindGameObjectsWithTag("RedPlatform");
        bluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");

        UpdatePlatforms();
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            redActive = !redActive;
            UpdatePlatforms();
        }
    }

    void UpdatePlatforms()
    {
        foreach (GameObject red in redPlatforms)
        {
            SetPlatformState(red, redActive);
        }

        foreach (GameObject blue in bluePlatforms)
        {
            SetPlatformState(blue, !redActive);
        }
    }

    void SetPlatformState(GameObject platform, bool active)
    {
        // Enable/disable collider
        Collider2D col = platform.GetComponent<Collider2D>();
        if (col) col.enabled = active;

        // Adjust opacity, keep original color
        SpriteRenderer sr = platform.GetComponent<SpriteRenderer>();
        if (sr)
        {
            Color originalColor = sr.color;
            originalColor.a = active ? 1f : SeeThoughtAmount;  // 1 = fully visible, 0.3 = faded
            sr.color = originalColor;
        }
    }
}
