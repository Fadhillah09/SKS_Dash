using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int currentStage = 1;

    // Buku
    public int baseBookTarget = 5;

    // Waktu
    public float baseTime = 60f;
    public float minTime = 20f;

    // Obstacle
    public float baseSpawnInterval = 2.5f;
    public float minInterval = 0.4f;

    // Hasil roll stage ini (dibaca oleh script lain)
    [HideInInspector] public int thisStageBookTarget;
    [HideInInspector] public float thisStageTime;
    [HideInInspector] public float thisStageSpawnInterval;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RollStage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RollStage()
    {
        int s = currentStage - 1;

        // Buku: stage 1=5, makin naik bebas, bisa +2 sampai +5 per stage
        thisStageBookTarget = baseBookTarget + s * Random.Range(2, 6);

        // Waktu: makin singkat, tidak pernah kurang dari minTime
        float timeReduction = s * Random.Range(3f, 7f);
        thisStageTime = Mathf.Max(baseTime - timeReduction, minTime);

        // Pensil: makin cepat spawn
        float intervalReduction = s * Random.Range(0.2f, 0.4f);
        thisStageSpawnInterval = Mathf.Max(baseSpawnInterval - intervalReduction, minInterval);

        Debug.Log($"Stage {currentStage} | Buku: {thisStageBookTarget} | Waktu: {thisStageTime:F0}s | Interval Pensil: {thisStageSpawnInterval:F2}s");
    }

    public int GetBookTarget() => thisStageBookTarget;
    public float GetTimeLimit() => thisStageTime;
    public float GetSpawnInterval() => thisStageSpawnInterval;

    public void NextStage()
    {
        currentStage++;
        RollStage();
    }

    public void ResetStage()
    {
        currentStage = 1;
        RollStage();
    }
}