using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject startCanvas;
    public GameObject gameCanvas;

    [Header("Game UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Audio")]
    public AudioClip burstSound;

    [Header("Spawning")]
    public GameObject balloonPrefab;
    public float spawnInterval = 1.5f;

    private int score = 0;
    private float elapsedTime = 0f;
    private bool isGameActive = false;

    void Start()
    {
        // Show Start screen, hide Game screen on launch
        if (startCanvas != null) startCanvas.SetActive(true);
        if (gameCanvas != null) gameCanvas.SetActive(false);

        if (scoreText != null) scoreText.text = "Score: 0";
        if (timerText != null) timerText.text = "Time: 0.0s";
    }

    void Update()
    {
        if (isGameActive)
        {
            elapsedTime += Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = "Time: " + elapsedTime.ToString("F1") + "s";
            }
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        elapsedTime = 0f;

        if (scoreText != null) scoreText.text = "Score: 0";

        // Hide Start screen, reveal Game screen
        if (startCanvas != null) startCanvas.SetActive(false);
        if (gameCanvas != null) gameCanvas.SetActive(true);

        InvokeRepeating(nameof(SpawnBalloon), 0.5f, spawnInterval);
    }

    void SpawnBalloon()
    {
        if (!isGameActive) return;

        float randomX = Random.Range(-3f, 3f);
        Vector3 spawnPos = new Vector3(randomX, -2f, 0f);
        Instantiate(balloonPrefab, spawnPos, Quaternion.identity);
    }

    public void UpdateScore(int amount)
    {
        if (!isGameActive) return;

        score += amount;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void PlayBurstSound(Vector3 position)
    {
        if (burstSound != null)
        {
            AudioSource.PlayClipAtPoint(burstSound, position);
        }
    }
}