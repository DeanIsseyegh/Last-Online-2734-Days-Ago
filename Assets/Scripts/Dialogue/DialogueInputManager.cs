using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueEditor
{
    public class DialogueInputManager : MonoBehaviour
    {
        public KeyCode[] m_UpKey;
        public KeyCode[] m_DownKey;
        public KeyCode[] m_SelectKey;

        private void Update()
        {
            if (ConversationManager.Instance != null)
            {
                UpdateConversationInput();
            }
        }

        private void UpdateConversationInput()
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (m_UpKey.Any(Input.GetKeyDown))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (m_DownKey.Any(Input.GetKeyDown))
                    ConversationManager.Instance.SelectNextOption();
                else if (m_SelectKey.Any(Input.GetKeyDown))
                    ConversationManager.Instance.PressSelectedOption();
            }
        }
    }
}
