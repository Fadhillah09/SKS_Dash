using UnityEngine;

public class BookPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance?.AddScore(1);
            Destroy(gameObject);
        }
    }
}