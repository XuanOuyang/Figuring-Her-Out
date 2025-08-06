using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Start point
    public Transform pointB; // End point
    public float speed = 2f; // Movement speed
    private Vector3 target;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Please assign both pointA and pointB in the inspector.");
            enabled = false;
            return;
        }

        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Switch target when close
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}