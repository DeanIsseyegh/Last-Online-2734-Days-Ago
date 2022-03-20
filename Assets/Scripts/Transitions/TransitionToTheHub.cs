using UnityEngine;
using UnityEngine.SceneManagement;

namespace Transitions
{
    public class TransitionToTheHub : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("Main Hub");
            }
        }
    }
}
