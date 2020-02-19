using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private ChestController m_chest = null;
    private TorchController m_torch = null;
    private bool m_canInteractChest = false;

    void Start()
    {
        m_torch = GetComponent<TorchController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_canInteractChest)
            {
                if (m_chest != null)
                {
                    if (m_chest.IsOpen())
                    {
                        Reward reward = m_chest.TakeReward();
                        m_torch.AddToFlame(reward.Amount);
                    }
                    else
                    {
                        m_chest.OpenChest();
                    }
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
            m_canInteractChest = true;
            m_chest = other.gameObject.transform.parent.gameObject.GetComponent<ChestController>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (IsChestTrigger(other.tag))
        {
            m_canInteractChest = false;
            m_chest = null;
        }
    }

    private bool IsChestTrigger(string tag)
    {
        return tag.ToLower() == "chesttrigger";
    }
}
