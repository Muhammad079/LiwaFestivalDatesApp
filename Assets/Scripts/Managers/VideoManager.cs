using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    private Action<string> m_PlayVideo;

    public string[] videoClipsPath;
    private string[] bgVideoFilePath;

    public VideoPlayer videoPlayer; 

    private string extension = ".mp4";

    public static VideoManager instance;
    private bool isVideoFinished = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is missing! Please assign it in the Inspector.");
            return;
        }

        m_PlayVideo += GetVideoAndPlay;
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void Start()
    {
        string videoPath = Application.streamingAssetsPath + "/ScreenSaver/0";
        ChangeVideo(videoPath, true);
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        isVideoFinished = true;
    }

    public void InvokeVideo(string videoPath)
    {
        m_PlayVideo?.Invoke(videoPath);
    }

    private void GetVideoAndPlay(string videoPath)
    {
        ChangeVideo(videoPath, false);
    }

    public void ChangeVideo(string videoPath, bool looping = true)
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Stop();

        videoPlayer.isLooping = true;
        videoPlayer.url = videoPath + extension;
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Stop();
    }

    public void StopVideoAndPlayIdle(string idleVideoPath)
    {
        ChangeVideo(idleVideoPath, true);
    }

    public void PlayVideoWithCallBack(string path, Action OnVideoCompleted)
    {
        StartCoroutine(WaitForVideoCompletion(path, OnVideoCompleted));
    }

    private IEnumerator WaitForVideoCompletion(string path, Action OnVideoCompleted)
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Stop();

        isVideoFinished = false;
        videoPlayer.isLooping = false;
        videoPlayer.url = path + extension;
        videoPlayer.Play();

        while (!isVideoFinished)
        {
            yield return null;
        }

        OnVideoCompleted?.Invoke();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }
    public void ResumeVideo()
    {
        if (videoPlayer == null) return;

        if (!videoPlayer.isPrepared)
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += vp => vp.Play();
            return;
        }

        if (videoPlayer.frame >= (long)videoPlayer.frameCount - 1)
            videoPlayer.frame = 0;

        videoPlayer.Play();
    }
}
