using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.OpenTipPanel("這裡昨天去過了，今天去別的地方看看吧!");
    }
}