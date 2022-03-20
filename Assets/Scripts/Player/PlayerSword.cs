using System;
using System.Security.Cryptography;
using Player;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject startPoint;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
        
        if (other.collider.CompareTag("Boss"))
        {
            other.collider.GetComponent<Health>().TakeDamage();
            playerController.MoveBackTo(startPoint.transform.position);
            other.collider.GetComponent<BossAi>().IncreaseThrowFrequency();
        }
    }


}
