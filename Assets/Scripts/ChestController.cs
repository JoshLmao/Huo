using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public float RewardAmount = 50f;

    private Animator m_animController = null;

    [SerializeField]
    private Animator m_boxAnimator;

    private bool m_isOpen = false;
    private bool m_hasTakenItem = false;
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
        StartCoroutine(WaitAndRise(1.5f));
        m_isOpen = true;
    }

    private IEnumerator WaitAndRise(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_boxAnimator.SetTrigger("onGiveReward");
    }

    public bool IsOpen()
    {
        return m_isOpen;
    }

    public Reward TakeReward()
    {
        if (m_boxAnimator != null)
        {
            m_hasTakenItem = true;
            
            foreach (Transform child in m_boxAnimator.transform)
                Destroy(child.gameObject);

            Destroy(m_boxAnimator);
        }

        return new Reward()
        {
            Amount = RewardAmount,
        };
    }
}
