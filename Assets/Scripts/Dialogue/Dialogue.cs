using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Dialogue : MonoBehaviour
{
    public NPCConversation _conversation;
    public Sprite sprite;

    private ConversationManager _conversationManager;

    // Start is called before the first frame update
    void Start()
    {
        _conversation = GetComponent<NPCConversation>();
        _conversationManager = ConversationManager.Instance;
        Conversation conversation = _conversation.Deserialize();
        List<NodeEventHolder> conversationNodeSerializedDataList = _conversation.NodeSerializedDataList;
        conversationNodeSerializedDataList.ForEach(it =>
            {
                it.Icon = sprite;
            }
        );
        // _conversation.GetNodeData(0).Icon = sprite;
        // _conversation.GetNodeData()
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            StartConvo();
        }
    }

    public void StartConvo()
    {
        Debug.Log("Starting convo");
        _conversationManager.StartConversation(_conversation);
    }
}