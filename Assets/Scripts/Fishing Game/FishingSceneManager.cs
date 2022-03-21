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

    void Update()
    {
        if (!_hasStartDialogueBeenShown)
        {
            _hasStartDialogueBeenShown = true;
            ConversationManager.Instance.StartConversation(startDialogue);
            ConversationManager.OnConversationEnded += StartFishingGameAfterXSeconds;
        }
    }

    private void StartFishingGameAfterXSeconds()
    {
        ConversationManager.OnConversationEnded -= StartFishingGameAfterXSeconds;
        fishingGame.SetActive(true);
    }

    public void OnWin()
    {
        PlayerPrefs.SetInt("HasWonFishing", 1);
        ConversationManager.Instance.StartConversation(endDialogue);
        ConversationManager.OnConversationEnded += GoBackToMainHub;
    }
    
    void GoBackToMainHub()
    {
        ConversationManager.OnConversationEnded -= GoBackToMainHub;
        SceneManager.LoadScene("Main Hub");
    }
}
