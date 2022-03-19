using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class NpcDialogueExample : MonoBehaviour
{
    private NPCConversation _npcConversation;

    // Start is called before the first frame update
    void Start()
    {
        _npcConversation = GetComponent<NPCConversation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("Collied with player");
            ConversationManager.Instance.StartConversation(_npcConversation);
        }
    }
}
