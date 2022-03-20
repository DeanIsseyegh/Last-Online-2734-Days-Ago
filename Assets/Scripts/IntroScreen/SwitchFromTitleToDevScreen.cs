using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchFromTitleToDevScreen : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoToMainHubScene()
    {
        SceneManager.LoadScene("Main Hub");
    }
}
