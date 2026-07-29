using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class InactivityTimer : MonoBehaviour
{
    public Button[] buttons;
    public float inactivityDuration = 120f;
    public float timer;
    public bool isFirstButtonActive = true;

    private string configFileName = "config.txt";

    void Start()
    {
        LoadConfig();
        ResetTimer();
        SetFirstButtonState(true);
        AutoSelectFirstButton();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnAnyButtonClicked(button));
        }
        VideoManager.instance.InvokeVideo(Application.streamingAssetsPath + $"/ScreenSaver/0");
    }

    void Update()
    {
        if (!isFirstButtonActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                AutoSelectFirstButton();
                ResetTimer();
            }
        }
    }

    private void LoadConfig()
    {
        string path = Path.Combine(Application.streamingAssetsPath, configFileName);
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (line.StartsWith("InactivityDuration="))
                {
                    string value = line.Replace("InactivityDuration=", "").Trim();
                    if (float.TryParse(value, out float result))
                    {
                        inactivityDuration = result;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning($"Config file not found at {path}, using default inactivityDuration {inactivityDuration}");
        }
    }

    private void ResetTimer()
    {
        timer = inactivityDuration;
    }

    private void OnAnyButtonClicked(Button clickedButton)
    {
        isFirstButtonActive = (clickedButton == buttons[0]);
        ResetTimer();
    }

    private void AutoSelectFirstButton()
    {
        if (buttons.Length > 0)
        {
            //buttons[0].onClick.Invoke();
            EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
            SetFirstButtonState(true);
        }
    }

    private void SetFirstButtonState(bool active)
    {
        isFirstButtonActive = active;
    }
}
