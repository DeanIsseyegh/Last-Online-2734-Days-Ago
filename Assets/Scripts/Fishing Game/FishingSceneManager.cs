using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingSceneManager : MonoBehaviour
{
    [SerializeField] private NPCConversation startDialogue;
    [SerializeField] private NPCConversation endDialogue;
    private bool _hasStartDialogueBeenShown;
    [SerializeField] private float gameStartsAfterXSeconds = 10f;
    [SerializeField] private GameObject fishingGame;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasStartDialogueBeenShown)
        {
            _hasStartDialogueBeenShown = true;
            ConversationManager.Instance.StartConversation(startDialogue);
            StartCoroutine(StartFishingGameAfterXSeconds());
        }
    }

    private IEnumerator StartFishingGameAfterXSeconds()
    {
        yield return new WaitForSeconds(gameStartsAfterXSeconds);
        fishingGame.SetActive(true);
    }

    public void OnWin()
    {
        PlayerPrefs.SetInt("HasWonFishing", 1);
        ConversationManager.Instance.StartConversation(endDialogue);
        StartCoroutine(GoBackToMainHub());
    }
    
    IEnumerator GoBackToMainHub()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Hub");
    }
}
