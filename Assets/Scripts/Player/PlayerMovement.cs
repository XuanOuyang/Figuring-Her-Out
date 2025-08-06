using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    [SerializeField] private Animator animator;

    float horizontalMove = 0f;
    bool jump = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        HandleMovement();

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (controller == null)
        {
            Debug.LogError("[PlayerMovement] controller is NULL!");
            return;
        }

        controller.Move(horizontalMove, false, jump);
        jump = false;
    }

    private void HandleMovement()
    {
        float input = Input.GetAxis("Horizontal");
        if (input != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}