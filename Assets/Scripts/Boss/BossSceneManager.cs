using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneManager : MonoBehaviour
{
    [SerializeField] private NPCConversation startingDialogue;
    [SerializeField] private NPCConversation endingDialgoue;
    [SerializeField] private GameObject mainHubTransition;
    [SerializeField] private BossAi bossAi;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;
    
    private bool _hasStartedStartingDialgoue;



    public void Update()
    {
        if (!_hasStartedStartingDialgoue)
        {
            playerController.isControlsEnabled = false;
            playerAttack.isControlsEnabled = false;
            _hasStartedStartingDialgoue = true;
            ConversationManager.Instance.StartConversation(startingDialogue);
            ConversationManager.OnConversationEnded += StartBossFight;
        }
    }

    private void StartBossFight()
    {
        bossAi.enabled = true;
        playerController.isControlsEnabled = true;
        playerAttack.isControlsEnabled = true;
        ConversationManager.OnConversationEnded -= StartBossFight;
    }

    public void WinBossFight()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(o);
        }

        GameObject boss = GameObject.FindWithTag("Boss");
        Destroy(boss);
        
        playerController.isControlsEnabled = false;
        playerController.dir = Vector2.zero;
        playerAttack.isControlsEnabled = false;
        
        ConversationManager.Instance.StartConversation(endingDialgoue);
        
        ConversationManager.OnConversationEnded += GoBackToMainHub;
        mainHubTransition.SetActive(true);
        PlayerPrefs.SetInt("HasWonCombat", 1);
    }
    
    private void GoBackToMainHub()
    {
        PlayerPrefs.SetString("MainHubExitPos", "East");
        ConversationManager.OnConversationEnded -= GoBackToMainHub;
        SceneManager.LoadScene("Main Hub");
    }
}