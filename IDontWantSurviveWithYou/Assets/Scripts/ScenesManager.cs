using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        // 加載新的場景
        SceneManager.LoadScene("Beach");
    }
}
