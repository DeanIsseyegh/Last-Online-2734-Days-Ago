using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
    private NPCConversation _conversation;

    private ConversationManager _conversationManager;

    // Start is called before the first frame update
    void Start()
    {
        _conversation = GetComponent<NPCConversation>();
        _conversationManager = ConversationManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _conversationManager.StartConversation(_conversation);
        }
    }
}
