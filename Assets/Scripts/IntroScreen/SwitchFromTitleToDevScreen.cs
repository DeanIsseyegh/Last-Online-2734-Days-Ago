using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchFromTitleToDevScreen : MonoBehaviour
{
    private bool shouldListenForTitleContinue;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (shouldListenForTitleContinue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetBool("StartLoading", true);
            }
        }
    }

    public void GoToMainHubScene()
    {
        SceneManager.LoadScene("Main Hub");
    }
    
    public void PressSpaceToContinue()
    {
        Debug.Log("Press Space To Continue");
        shouldListenForTitleContinue = true;
    }
}
