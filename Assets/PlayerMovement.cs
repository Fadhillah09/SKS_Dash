using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Sprite deadSprite;
    public Sprite idleSprite;
    public Sprite runSprite;
    public Sprite jumpSprite;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isDead = false;
    private bool isGrounded = false;
    [HideInInspector] public bool isOnLadder = false;

    // Coyote time
    private float coyoteTime = 0.2f;
    private float coyoteCounter = 0f;
    private bool coyoteGrounded = false;

    // Double jump
    private int jumpCount = 0;
    public int maxJumps = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;
        coyoteGrounded = coyoteCounter > 0f;

        float moveX = 0f;

        if (!isOnLadder)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                moveX = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                moveX = 1f;
        }

        if (!isOnLadder)
            rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (moveX > 0) transform.localScale = new Vector3(0.46f, 0.46f, 0.46f);
        else if (moveX < 0) transform.localScale = new Vector3(-0.46f, 0.46f, 0.46f);

        // Ganti sprite
        if (!coyoteGrounded)
        {
            if (jumpSprite != null) sr.sprite = jumpSprite;
        }
        else if (moveX == 0)
        {
            if (idleSprite != null) sr.sprite = idleSprite;
        }
        else
        {
            if (runSprite != null) sr.sprite = runSprite;
        }

        // Double jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isOnLadder && jumpCount < maxJumps)
        {
            coyoteCounter = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (deadSprite != null)
            sr.sprite = deadSprite;

        rb.linearVelocity = Vector2.zero;
        this.enabled = false;

        // TAMBAHAN: trigger game over ke ScoreManager
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.GameOver();
        else
            Invoke("ReloadScene", 1.5f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}