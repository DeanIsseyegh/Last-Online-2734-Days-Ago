using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject winTitle;
    [SerializeField] private NPCConversation startingDialogue;
    [SerializeField] private NPCConversation endingDialgoue;
    [SerializeField] private GameObject mainHubTransition;
    
    private bool _hasStartedStartingDialgoue;

    private void Start()
    {
        
    }

    public void Update()
    {
        if (!_hasStartedStartingDialgoue)
        {
            _hasStartedStartingDialgoue = true;
            ConversationManager.Instance.StartConversation(startingDialogue);
        }
    }

    public void WinBossFight()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(o);
        }

        GameObject boss = GameObject.FindWithTag("Boss");
        Destroy(boss);
        
        // winTitle.SetActive(true);
        ConversationManager.Instance.StartConversation(endingDialgoue);
        mainHubTransition.SetActive(true);
        PlayerPrefs.SetInt("HasWonCombat", 1);
        StartCoroutine(GoBackToMainHub());
    }
    
    IEnumerator GoBackToMainHub()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Hub");
    }
}