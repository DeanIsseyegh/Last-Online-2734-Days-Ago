using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private float attackDuration = 0.5f;

    [SerializeField] private float attackCooldown = 1.5f;

    private bool _isAttacking;
    private float _currentAttackDuration;
    private float _timeSinceLastAttack = 999f;
    public bool isControlsEnabled;

    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isControlsEnabled) return;

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
        _animator.SetTrigger("AttackTrigger");
        playerWeapon.SetActive(true);
        _isAttacking = true;
        _playerController.isControlsEnabled = false;
        _playerController.dir = Vector2.zero;
        yield return new WaitForSeconds(attackDuration);
        _playerController.isControlsEnabled = true;
        playerWeapon.SetActive(false);
        _isAttacking = false;
        _timeSinceLastAttack = 0;
    }
}