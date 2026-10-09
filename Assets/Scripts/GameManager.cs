using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI timerText; 

    [Header("Audio")]
    public AudioClip burstSound;

    [Header("Spawning")]
    public GameObject balloonPrefab;
    public float spawnInterval = 1.5f; 
    public int balloonsPerSpawn = 3; 

    [Header("Appearance")]
    public Material[] balloonMaterials; 

    private int score = 0;
    private float elapsedTime = 0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBalloon), 1f, spawnInterval);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = "Time: " + elapsedTime.ToString("F1") + "s";
        }
    }

    void SpawnBalloon()
    {
        for (int i = 0; i < balloonsPerSpawn; i++)
        {
            float randomX = Random.Range(-3.5f, 3.5f);
            float randomY = Random.Range(-2.5f, -1.0f);
            
            Vector3 spawnPos = new Vector3(randomX, randomY, 0f);
            GameObject newBalloon = Instantiate(balloonPrefab, spawnPos, Quaternion.identity);

            if (balloonMaterials != null && balloonMaterials.Length > 0)
            {
                Material randomMat = balloonMaterials[Random.Range(0, balloonMaterials.Length)];
                newBalloon.GetComponent<MeshRenderer>().material = randomMat;
            }
        }
    }

    public void UpdateScore(int amount)
    {
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