using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ApplicationManager : MonoBehaviour
{
    public VideoPlayer backgroundVideoPlayer; // Assign in Inspector

    private void Start()
    {
        Application.targetFrameRate = 90;

        //VideoManager.instance.InvokeVideo(StreamingAssetsManager.Instance.GetFilePath("/Videos/IdleVideo/0"));

        UIManager.Instance.ToggleScreen(UIManager.OpeningScreen);

        //StartBackgroundVideo();
        foreach (var item in Display.displays)
        {
            item.Activate();
        }
    }

    void StartBackgroundVideo()
    {
        if (backgroundVideoPlayer == null)
        {
            Debug.LogError("No VideoPlayer assigned for background video.");
            return;
        }

        //backgroundVideoPlayer.Stop();
        //backgroundVideoPlayer.isLooping = true;
        //backgroundVideoPlayer.url = StreamingAssetsManager.Instance.GetFilePath("/Videos/BgUIVideo/0") + ".mp4";
        //backgroundVideoPlayer.Play();
    }
}
