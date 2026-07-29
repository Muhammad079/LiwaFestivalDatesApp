using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitListVideoPlayer : AppScreens
{
    public List<Button> Fruit_btns;
    public Button PlayPauseBtn;
    private void Awake()
    {
        for (int i = 0; i < Fruit_btns.Count; i++) 
        {
            int index = i;

            Fruit_btns[index].onClick.AddListener(() => 
            {
                VideoManager.instance.InvokeVideo(Application.streamingAssetsPath + $"/FruitVideos/{index}");
                Debug.LogError($"Playing Video at path: Application.streamingAssetsPath + /FruitVideos/{index} ");
            });
        }

        PlayPauseBtn.onClick.AddListener(() => HandleVideoPlayer());
    }

    bool isPaused = true;
    private void HandleVideoPlayer()
    {
        isPaused = !isPaused;
        if (!isPaused)
        {
            Debug.LogError("Play");
            VideoManager.instance.PauseVideo();
            PlayPauseBtn.GetComponentInChildren<TMPro.TMP_Text>().text = "Play";
        }
        else
        {
            Debug.LogError("Pause");
            VideoManager.instance.ResumeVideo();
            PlayPauseBtn.GetComponentInChildren<TMPro.TMP_Text>().text = "Pause";
        }
    }
}
