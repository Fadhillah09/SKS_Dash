using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stageText;

    private int score = 0;
    private int targetScore = 5;
    private float timeLeft;
    private bool isGameOver = false;
    private bool isGameClear = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (StageManager.Instance != null)
        {
            targetScore = StageManager.Instance.GetBookTarget();
            timeLeft = StageManager.Instance.GetTimeLimit();
        }
        else
        {
            targetScore = 5;
            timeLeft = 60f;
        }

        Time.timeScale = 1f;
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver || isGameClear) return;

        timeLeft -= Time.deltaTime;
        UpdateTimerUI();

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver || isGameClear) return;

        score += amount;
        UpdateUI();

        if (score >= targetScore)
            GameClear();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Buku: " + score + " / " + targetScore;

        if (stageText != null && StageManager.Instance != null)
            stageText.text = "Stage: " + StageManager.Instance.currentStage;
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int detik = Mathf.CeilToInt(timeLeft);
            timerText.text = "Waktu: " + detik + "s";
            timerText.color = timeLeft <= 10f ? Color.red : Color.white;
        }
    }

    void GameClear()
    {
        isGameClear = true;

        if (scoreText != null)
        {
            scoreText.text = "Stage " + StageManager.Instance.currentStage + " Selesai!";
            scoreText.fontSize = 28;
        }

        if (StageManager.Instance != null)
            StageManager.Instance.NextStage();

        // Pakai Coroutine + WaitForSecondsRealtime agar tidak terpengaruh timeScale
        StartCoroutine(LoadAfterDelay(2f, false));
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (StageManager.Instance != null)
            StageManager.Instance.ResetStage();

        if (scoreText != null)
        {
            scoreText.text = "GAME OVER!";
            scoreText.fontSize = 36;
            scoreText.color = Color.red;
        }

        StartCoroutine(LoadAfterDelay(2f, true));
    }

    IEnumerator LoadAfterDelay(float delay, bool isGameOverScene)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}