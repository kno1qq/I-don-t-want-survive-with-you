using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ToLobby : MonoBehaviour
{
    public string nextSceneName; // �]�w�n�����쪺�����W��
    private VideoPlayer videoPlayer;

    void Start()
    {
        // ��� VideoPlayer �ե�
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // �q�\ loopPointReached �ƥ�
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("����� VideoPlayer �ե�");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ��v�����񧹲��ɤ�������
        SceneManager.LoadScene(nextSceneName);
    }

    void OnDestroy()
    {
        // �T�O�b�P���}���ɨ����q�\�ƥ�A�קK���s���|
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
