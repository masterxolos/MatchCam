using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider sldLevelPassRate = null;

    [SerializeField] private Transform[] targetNodes;
    [SerializeField] private Transform[] currentNodes;

    public static GameManager instance;
    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

    }

    private void Update()
    {
        CalculatePassRate();
    }

    public void CalculatePassRate()
    {
        float sum = 0;
        for (int i = 0; i < targetNodes.Length; i++)
        {
            float distance = Vector3.Distance(targetNodes[i].position, currentNodes[i].position + new Vector3(40f, 0f, 0f));

            if (distance > 1f)
                sum += 1f / targetNodes.Length;
            else
                sum += distance / targetNodes.Length;
        }

        sldLevelPassRate.value = 1f - sum; 

    }
}
