using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoRestartManager : MonoBehaviour
{
    public float inactivityDuration = 60f; // Seconds
    private float timer = 0f;
    private bool isReloading = false;

    void Start()
    {
        timer = 0f;

        // Hook into all buttons
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(ResetTimer);
        }
    }

    void Update()
    {
        if (isReloading) return;

        timer += Time.deltaTime;

        // Use a small grace time to prevent race condition (like 0.1f buffer)
        if (timer >= inactivityDuration + 0.1f)
        {
            isReloading = true;
            RestartScene();
        }
    }

    void ResetTimer()
    {
        // If reset happens just before reloading, cancel reload
        if (isReloading)
        {
            isReloading = false;
        }

        timer = 0f;
    }

    void RestartScene()
    {
        // Optional: Add a fade or log
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
