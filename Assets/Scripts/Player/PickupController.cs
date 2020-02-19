using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private ChestController m_chest = null;
    private bool m_canOpenChest = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed!");

            if (m_canOpenChest)
            {
                if (m_chest != null)
                {
                    Debug.Log("Can open a chest");
                    m_chest.OpenChest();
                }
                else
                {
                    Debug.LogError("Unable to open chest - Lost reference");
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (IsChestTrigger(other.tag))
        {
            m_canOpenChest = true;
            m_chest = other.gameObject.transform.parent.gameObject.GetComponent<ChestController>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (IsChestTrigger(other.tag))
        {
            m_canOpenChest = false;
            m_chest = null;
        }
    }

    private bool IsChestTrigger(string tag)
    {
        return tag.ToLower() == "chesttrigger";
    }
}
