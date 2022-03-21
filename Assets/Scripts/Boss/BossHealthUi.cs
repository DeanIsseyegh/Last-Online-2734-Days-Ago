using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHealthUi : MonoBehaviour
{
    [SerializeField] private GameObject hp1;
    [SerializeField] private GameObject hp2;
    [SerializeField] private GameObject hp3;

    public void SetHealth(int heath)
    {
        if (heath == 2)
        {
            hp3.SetActive(false);
        }
        
        if (heath == 1)
        {
            hp2.SetActive(false);
        }
        
        if (heath == 0)
        {
            hp1.SetActive(false);
        }

    }
}
