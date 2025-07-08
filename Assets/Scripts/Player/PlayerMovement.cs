using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));

        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }
    void FixedUpdate()
    {
        Debug.Log($"[PlayerMovement] FixedUpdate – horiz:{horizontalMove}, jump:{jump}");
        controller.Move(horizontalMove, false, jump);
        jump = false;

        if (controller == null)
        {
            Debug.LogError("[PlayerMovement] controller is NULL!");
            return;
        }
    }
}
