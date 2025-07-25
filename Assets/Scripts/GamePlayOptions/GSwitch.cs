using UnityEngine;

public class GSwitch : MonoBehaviour
{
    public float waitForSeconds = 5f;
    public Color normalColor = Color.gray;
    public Color lowGColor = Color.red;

    public float normalGravity = 1;
    public float lowGravity = 1;
    public float normalJumpForce = 15f;
    public float lowJumpForce = -15f; // negative because gravity is reversed
    public bool allowAirControl = true;
    public CharacterController2D characterController; // Reference to player controller
    public Rigidbody2D playerRigidbody;              // Reference to player Rigidbody

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
            characterController.SetAirControl(allowAirControl); // ← Disable air control
        }
        else
        {
            playerRigidbody.gravityScale = normalGravity;
            Camera.main.backgroundColor = normalColor;
            characterController.SetJumpForce(normalJumpForce);
            characterController.SetAirControl(true); // ← Enable air control
        }
    }
}
