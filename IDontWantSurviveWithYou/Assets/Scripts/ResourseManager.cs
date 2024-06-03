using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseManager : MonoBehaviour
{
    public static ResourseManager instance;

    private void Awake()
    {
        instance = this;
    }

    public PopupText popupText;
}
