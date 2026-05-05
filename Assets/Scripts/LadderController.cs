using UnityEngine;
using UnityEngine.InputSystem;

public class LadderController : MonoBehaviour
{
    private bool isNearLadder = false; // dekat tangga
    private bool isOnLadder = false;  // sedang naik tangga
    private Rigidbody2D rb;
    private PlayerMovement pm;
    public float climbSpeed = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Mulai naik tangga hanya saat W/Up ditekan dan dekat tangga
        if (isNearLadder && !isOnLadder)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            {
                isOnLadder = true;
                pm.isOnLadder = true;
                rb.gravityScale = 0f;
            }
        }

        if (!isOnLadder) return;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0f, climbSpeed);
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0f, -climbSpeed);
        }
        else
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnDisable()
    {
        isOnLadder = false;
        isNearLadder = false;
        if (pm != null) pm.isOnLadder = false;
        if (rb != null) rb.gravityScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
            isNearLadder = true; // hanya tandai dekat tangga
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = false;
            isOnLadder = false;
            pm.isOnLadder = false;
            rb.gravityScale = 1f;
        }
    }
}