using UnityEngine;

public class GSwitch : MonoBehaviour
{
    public float waitForSeconds = 5f;
    public Color normalColor = Color.gray;
    public Color lowGColor = Color.red;

    public float normalGravity = 1;
    public float lowGravity = 1;
    public float normalJumpForce = 15f;
    public float lowJumpForce = -15f;
    public bool allowAirControl = true;

    public CharacterController2D characterController;
    public Rigidbody2D playerRigidbody;
    public PlayerMovement playerMovement;

    public float normalRunSpeed = 40f;
    public float lowRunSpeed = 20f;

    private bool gravityReversed = false;

    void Start()
    {
        StartCoroutine(SwitchGravityRoutine());
    }

    System.Collections.IEnumerator SwitchGravityRoutine()
    {
        yield return new WaitForSeconds(waitForSeconds);

        while (true)
        {
            SwitchGravity();
            yield return new WaitForSeconds(waitForSeconds);
        }
    }

    void SwitchGravity()
    {
        gravityReversed = !gravityReversed;

        if (gravityReversed)
        {
            playerRigidbody.gravityScale = lowGravity;
            Camera.main.backgroundColor = lowGColor;
            characterController.SetJumpForce(lowJumpForce);
            characterController.SetAirControl(allowAirControl);
        }
        else
        {
            playerRigidbody.gravityScale = normalGravity;
            Camera.main.backgroundColor = normalColor;
            characterController.SetJumpForce(normalJumpForce);
            characterController.SetAirControl(true);
        }
    }

    void Update()
    {
        if (!characterController.IsGrounded())
        {
            // Player is in the air
            playerMovement.runSpeed = gravityReversed ? lowRunSpeed : normalRunSpeed;
        }
        else
        {
            // Player is grounded
            playerMovement.runSpeed = normalRunSpeed;
        }
    }
}
