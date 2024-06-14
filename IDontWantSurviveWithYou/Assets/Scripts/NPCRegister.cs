using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRegister : MonoBehaviour
{
    private void Start()
    {
        NPCInteraction npcInteraction = GetComponent<NPCInteraction>();
        if (npcInteraction != null)
        {
            GameManager.Instance.RegisterNPC(npcInteraction);
        }
    }
}
