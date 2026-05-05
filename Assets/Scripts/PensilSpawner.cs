using UnityEngine;

public class PensilSpawner : MonoBehaviour
{
    public GameObject pensilPrefab;

    public float spawnY = 7f;
    public float minX = -7f;
    public float maxX = 7f;

    private float spawnInterval = 2.5f;
    private float minInterval = 0.4f;
    private float timer;

    private float currentFallSpeed = 4f;
    private float maxFallSpeed = 15f;

    private float difficultyTimer = 0f;
    private float difficultyIncreaseEvery = 5f;

    void Start()
    {
        // TAMBAHAN: ambil interval awal dari StageManager
        if (StageManager.Instance != null)
        {
            spawnInterval = StageManager.Instance.GetSpawnInterval();
            // Stage makin tinggi = pensil makin cepat dari awal
            currentFallSpeed = Mathf.Min(4f + (StageManager.Instance.currentStage - 1), maxFallSpeed);
        }

        timer = spawnInterval;
    }

    void Update()
    {
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= difficultyIncreaseEvery)
        {
            difficultyTimer = 0f;
            spawnInterval = Mathf.Max(spawnInterval - 0.15f, minInterval);
            currentFallSpeed = Mathf.Min(currentFallSpeed + 1f, maxFallSpeed);

            Debug.Log("Difficulty naik! Speed: " + currentFallSpeed +
                      " | Interval: " + spawnInterval);
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnPensil();
            timer = spawnInterval;
        }
    }

    void SpawnPensil()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(randomX, spawnY, 0f);

        GameObject pensil = Instantiate(pensilPrefab, pos, Quaternion.identity);

        PensilObstacle po = pensil.GetComponent<PensilObstacle>();
        if (po != null)
            po.fallSpeed = currentFallSpeed;
    }
}