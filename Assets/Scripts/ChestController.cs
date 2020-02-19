using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator m_animController = null;

    private bool m_isOpen = false;

    void Start()
    {
        m_animController = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void OpenChest()
    {
        if (m_isOpen)
            return;

        m_animController.SetTrigger("onOpenChest");
        m_isOpen = true;
    }
}
