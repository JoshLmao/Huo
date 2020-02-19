using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform MasterObject;

    [SerializeField]
    private GameObject m_chestPrefab;

    private List<GameObject> m_chests = new List<GameObject>();
    private int m_takenRewards = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in MasterObject.transform)
        {
            //GameObject inst = Instantiate(m_chestPrefab, t.position, t.rotation, MasterObject.transform);
            //m_chests.Add(inst);

            
        }

        foreach (Transform child in MasterObject.transform)
        {
            Debug.Log(child.gameObject);
            var inst= Instantiate(m_chestPrefab, child.gameObject.transform);
            m_chests.Add(inst);

            float amount = UnityEngine.Random.Range(20f, 75f);
            inst.GetComponent<ChestController>().RewardAmount = amount;

            inst.GetComponent<ChestController>().OnRewardTaken += OnRewardTaken;
        }
    }

    private void OnRewardTaken()
    {
        m_takenRewards++;

        if (m_takenRewards == m_chests.Count)
        {
            Debug.Log("You WIN!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
