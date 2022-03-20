using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationFlash : MonoBehaviour
{
    private Image _notification;
    private float flashFrequency = 0.25f;
    private float flashTimer = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        _notification = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0)
        {
            _notification.enabled = !_notification.enabled;
            flashTimer = flashFrequency;
        }
    }

}
