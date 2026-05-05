using UnityEngine;

public class PensilObstacle : MonoBehaviour
{
    public float fallSpeed = 6f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * fallSpeed;
        rb.angularVelocity = Random.Range(-60f, 60f);
    }

    void Update()
    {
        if (transform.position.y < -10f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        // Panggil fungsi mati di player
        other.GetComponent<PlayerMovement>().Die();
        Destroy(gameObject);
    }
}
}