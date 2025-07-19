using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    float jumpPressedTime;
    bool jumpInput;

    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
            jumpPressedTime = Time.time;
        }

        controller.Move(direction);
        controller.Jump(ref jumpInput, ref jumpPressedTime);
        Debug.Log("Direction: " + direction);
    }
}