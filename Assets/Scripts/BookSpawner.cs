using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    public GameObject bookPrefab;
    public Transform[] spawnPoints; // titik spawn di tiap ground
    public float respawnTime = 10f; // buku respawn tiap 10 detik

    private float timer;
    private int maxBooks = 5;

    void Start()
    {
        if (StageManager.Instance != null)
        {
            maxBooks = StageManager.Instance.GetBookTarget();
            // Stage tinggi = buku respawn lebih cepat
            respawnTime = Mathf.Max(10f - (StageManager.Instance.currentStage - 1), 3f);
        }

        SpawnAllBooks();
        timer = respawnTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnRandomBook();
            timer = respawnTime;
        }
    }

    void SpawnAllBooks()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        int count = Mathf.Min(maxBooks, spawnPoints.Length);
        for (int i = 0; i < count; i++)
        {
            SpawnAt(spawnPoints[i]);
        }
    }

    void SpawnRandomBook()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        SpawnAt(spawnPoints[randomIndex]);
    }

    void SpawnAt(Transform point)
    {
        if (bookPrefab != null && point != null)
            Instantiate(bookPrefab, point.position, Quaternion.identity);
    }
}