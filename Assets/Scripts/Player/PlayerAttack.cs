using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private float attackDuration = 0.5f;

    [SerializeField] private float attackCooldown = 1.5f;

    private bool _isAttacking;
    private float _currentAttackDuration;
    private float _timeSinceLastAttack = 999f;

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (_isAttacking) return;
        
        if (_timeSinceLastAttack > attackCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(StartAttack());
            }
        }
    }

    private IEnumerator StartAttack()
    {
        playerWeapon.SetActive(true);
        _isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        playerWeapon.SetActive(false);
        _isAttacking = false;
        _timeSinceLastAttack = 0;
    }
}