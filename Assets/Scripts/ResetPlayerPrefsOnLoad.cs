using UnityEngine;

public class ResetPlayerPrefsOnLoad : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

}
