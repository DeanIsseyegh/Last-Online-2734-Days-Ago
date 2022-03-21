using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int bossMaxHealth = 3;
    [SerializeField] private BossHealthUi bossHealthUi;
    [SerializeField] private BossSceneManager bossSceneManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damagedSound;
    private int currentHealth;
    void Start()
    {
        currentHealth = bossMaxHealth;
        bossHealthUi.SetHealth(currentHealth);
    }


    public void TakeDamage()
    {
        audioSource.PlayOneShot(damagedSound);
        currentHealth--;
        bossHealthUi.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            bossSceneManager.WinBossFight();
        }
    }
}
