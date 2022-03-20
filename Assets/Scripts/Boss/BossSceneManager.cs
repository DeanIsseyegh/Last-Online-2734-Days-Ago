using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject winTitle;
    // Start is called before the first frame update
    public void WinBossFight()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(o);
        }

        GameObject boss = GameObject.FindWithTag("Boss");
        Destroy(boss);
        
        winTitle.SetActive(true);
    }
}