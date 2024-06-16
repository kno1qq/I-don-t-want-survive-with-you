using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ToLobby : MonoBehaviour
{
    public string nextSceneName; // 設定要切換到的場景名稱
    private VideoPlayer videoPlayer;

    void Start()
    {
        // 獲取 VideoPlayer 組件
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // 訂閱 loopPointReached 事件
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("未找到 VideoPlayer 組件");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 當影片播放完畢時切換場景
        SceneManager.LoadScene(nextSceneName);
    }

    void OnDestroy()
    {
        // 確保在銷毀腳本時取消訂閱事件，避免內存洩漏
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
