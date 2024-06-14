using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScreenPanel : MonoBehaviour
{
    public bool isClose;
    private void OnEnable()
    {
        isClose = false;
    }
    public void close()
    {
        gameObject.SetActive(false);
        isClose = true;
    }
}
