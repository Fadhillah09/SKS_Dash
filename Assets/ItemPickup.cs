using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int pointValue = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger kena: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Buku diambil!");

            // Cek dulu apakah GameManager ada
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(pointValue);
            }

            Destroy(gameObject);
        }
    }
}