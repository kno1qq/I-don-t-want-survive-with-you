using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Item food;
    public Item Water;
    public Item nife;
    public Item key;
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        // 加載新的場景
        SceneManager.LoadScene("Beach");
        food.itemHeld = 1;
        Water.itemHeld = 1;
        nife.itemHeld = 1;
        key.itemHeld = 1;
    }
}
