using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchFromTitleToDevScreen : MonoBehaviour
{
    private bool _shouldListenForTitleContinue;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_shouldListenForTitleContinue)
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
        _shouldListenForTitleContinue = true;
    }
}
