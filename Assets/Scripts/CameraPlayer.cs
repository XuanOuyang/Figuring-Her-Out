using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] bool lockRotation = true;
    [SerializeField] bool smoothCamera = true;

    [Header("SMOOTH CAMERA")]
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] Transform target;

    [Header("OFFSET")]
    [SerializeField] PlayerController player;
    [SerializeField] float xOffset = 1f;

    Vector3 velocity = Vector3.zero;
    //The default 2D camera's Z is -10, this is to prevent it breaking.
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 offset = new Vector3(player.horDirection * xOffset, 0, -10f);
        if (lockRotation)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z, 0, 0));

        if (smoothCamera)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

    }
}