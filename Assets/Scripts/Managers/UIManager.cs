using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// these all are the default screens. Dont work here
/// </summary>
public class UIManager : MonoBehaviour
{
    public AppScreens[] Screens;
    public static UIManager Instance;
    Dictionary<string, AppScreens> ApplicationScreens = new Dictionary<string, AppScreens>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Screens = GetComponentsInChildren<AppScreens>(true);
        if (Screens.Length > 0)
        {
            foreach (var screen in Screens)
            {
                ApplicationScreens.Add(screen.name, screen);
                screen.gameObject.SetActive(false);
            }
        }
    }
    public void ToggleScreen(string p_ScreenName)
    {
        foreach (var screen in Screens)
        {
            if (screen.ScreenName == p_ScreenName)
            {
                var item = screen.TargetObject;
                if (item != null)
                {
                    item.SetActive(true);
                    screen.gameObject.SetActive(true);
                }
                else
                {
                    screen.gameObject.SetActive(true);
                }
                screen.canvasGroup.alpha = 0;
                StartCoroutine(Extensions.FadeIn(screen.canvasGroup, 1));
            }
            else
            {
                StartCoroutine(Extensions.FadeOut(screen.canvasGroup, 1));
                screen.gameObject.SetActive(false);
                
            }
        }
    }
    public static string OpeningScreen = "OpeningScreen";
    public static string SelectionScreen = "SelectionScreen";
    public static string EndVideoScreen = "EndVideoScreen";
}
