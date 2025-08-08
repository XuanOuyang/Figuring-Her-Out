using System.Collections;
using UnityEngine;

public class TextAppear : MonoBehaviour
{
    public GameObject player;
    public GameObject appearObject;

    private bool triggered = false;
    public float showtime = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject == player)
        {
            triggered = true;
            StartCoroutine(ShowTextBox()); // ✅ start the coroutine
        }
    }

    IEnumerator ShowTextBox()
    {
        // Show object
        if (appearObject != null)
            appearObject.SetActive(true);

        yield return new WaitForSeconds(showtime); // show time

        // Hide object
        if (appearObject != null)
            appearObject.SetActive(false);

        yield return new WaitForSeconds(3f); // optional extra wait after hiding
    }
}