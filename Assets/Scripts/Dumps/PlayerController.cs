using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f;

    [Header("JUMP")]
    [SerializeField] float jumpForce = 21f;
    [SerializeField] float gravity = 6f;
    [SerializeField] float fallMultiplier = 7f;
    [SerializeField] float lowJumpGravity = 15f;
    [SerializeField] public string groundLayerName = "Obstacle";

    [Header("EXTRA JUMP FEATURES")]
    [SerializeField] bool clampFall = true;
    [SerializeField] float maxFallSpeed = 25f;
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] float coyoteThreshold = 0.1f;

    [Header("APEX MOD")]
    [SerializeField] bool modifyApex = true;
    [SerializeField] float _apexBonus = 3f;
    [SerializeField] float minGravity = 5f;

    Rigidbody2D rb;
    bool grounded;
    bool jumped;
    float apexPoint;
    float timeLeftGround;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CalculateApex();
    }

    void FixedUpdate()
    {
        float apexGravity = Mathf.Lerp(gravity, minGravity, apexPoint);


        if (rb.velocity.y < 0)
            rb.gravityScale = fallMultiplier;
        //NOTE: If you're being "propelled" or moved upwards by an external force, pressing the jump button will increase your gravity, a way around this could be to check if you are being influenced by an external force and not increase gravity if so.
        //The way this works is: you're going up, you stop pressing the button, you have more gravity, you don't reach the original jump apex.
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.gravityScale = lowJumpGravity;
        else if (!grounded && modifyApex)
            rb.gravityScale = apexGravity;
        else
            rb.gravityScale = gravity;

        if (clampFall)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, Mathf.Infinity));

        Debug.Log("Gravity: " + rb.gravityScale);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && col.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            grounded = true;
            jumped = false;
        }
    }

    //To fix a stuck on the ground bug after sliding off a ramp
    void OnTriggerStay2D(Collider2D col)
    {
        if (col != null && col.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            grounded = true;
            jumped = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        grounded = false;
        timeLeftGround = Time.time;
    }

    public float horDirection { get; private set; }


    //TODO Small Acceleration (toggleable)
    //TODO Add a "jumped" check to make sure the apex mod is only activated after a jump
    public void Move(float direction)
    {
        horDirection = direction;

        float apexBonus = direction * _apexBonus * apexPoint;
        float moveSpeed = direction * _moveSpeed;
        if (modifyApex)
            moveSpeed += apexBonus;

        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void Jump(ref bool jumpInput, ref float jumpPressedTime)
    {
        bool canJump = jumpInput && grounded;
        bool canBufferJump = grounded && jumpPressedTime + jumpBufferTime > Time.time;
        bool canCoyote = !grounded && jumpInput && !jumped && timeLeftGround + coyoteThreshold > Time.time;

        if (canJump || canBufferJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //Doing this to prevent multiple jumps
            jumpPressedTime = 0;
            jumped = true;

        }
        else if (canCoyote)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            timeLeftGround = 0;
            jumped = true;
        }
        jumpInput = false;
    }

    void CalculateApex()
    {
        //A value used to reduce gravity and increase speed at the top of a jump
        if (!grounded)
            apexPoint = Mathf.InverseLerp(jumpForce, 0, Mathf.Abs(rb.velocity.y));
    }
}