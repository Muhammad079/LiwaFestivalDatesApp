using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamingAssetsManager : MonoBehaviour
{
    public static StreamingAssetsManager Instance;
    public static string StreamingAssetPath;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        StreamingAssetPath = Application.streamingAssetsPath;
    }

    /// <summary>
    /// use this method to get the path using / and all path should start with /
    /// autodetect streaming asset folder path. provide the path after that.
    /// </summary>
    /// <param name="path">use this method to get the path using / and all path should start with /</param>
    /// <returns></returns>
    internal string GetFilePath(string path)
    {
        var filePath = path.Split('/');
        string FinalPath = StreamingAssetPath;

        if(filePath.Length > 0)
        {
            foreach(var item in filePath)
            {
                FinalPath = Path.Combine(FinalPath, item);
            }
        }
        string fullPath = Path.Combine(Application.streamingAssetsPath, FinalPath).Replace("\\", "/");
        return fullPath;
    }
    /// 
    /// </summary>
    /// <param name="path">Automatically detects the path to the StreamingAssets folder 
    /// and combines it with the provided relative path to form the full file path.</param>
    /// <returns></returns>
    internal Sprite LoadImageFromStreamingPath(string path)
    {
        string filePath = GetFilePath(path);
        string fullPath = Path.Combine(Application.streamingAssetsPath, path).Replace("\\", "/");

        if (File.Exists(filePath))
        {
            byte[] ImageBytes = File.ReadAllBytes(fullPath);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(ImageBytes);

            Sprite finalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return finalSprite;
        }
        else
        {
            Debug.LogWarning("There was a problem getting the sprite from Directory " + filePath);
            return null;
        }

    }
}
