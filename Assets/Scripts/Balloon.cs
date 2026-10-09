using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float floatSpeed = 2.0f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Move the balloon upward
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // Destroy the balloon if it goes too high (missed)
        if (transform.position.y >= 4.0f)
        {
            if (gameManager != null)
            {
                gameManager.UpdateScore(-1);
            }
            Destroy(gameObject);
        }

        // NEW MANUAL CLICK DETECTION
        if (Input.GetMouseButtonDown(0)) // 0 is Left Mouse Button
        {
            // Create a ray from the camera to where the mouse is pointing on screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Shoot the ray. If it hits a 3D object with a collider...
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object we hit is THIS specific balloon
                if (hit.collider.gameObject == gameObject)
                {
                    if (gameManager != null)
                    {
                        gameManager.UpdateScore(1);
                        gameManager.PlayBurstSound(transform.position);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}