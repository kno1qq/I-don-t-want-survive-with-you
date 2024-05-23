using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPanel : MonoBehaviour
{
    public void close()
    {
        gameObject.SetActive(false);
    }
}
