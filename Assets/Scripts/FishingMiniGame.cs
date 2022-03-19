using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    [SerializeField] Transform topPivot, bottomPivot, fish;

    float fishPosition, fishDestination;

    float fishTimer;
    [SerializeField] float timerMultiplier = 3f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;
    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f, hookGravityPower = 0.005f, hookProgressDegradationPower = 0.1f;
    [SerializeField] SpriteRenderer hookSpriteRenderer;

    private void Start()
    {
        Resize();
    }
    void Resize()
    {

    }

    private void Update()
    {
        Fish();
        Hook();
    }
    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        hookPosition += hookPullVelocity;
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
    }

    private void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplier;
            fishDestination = UnityEngine.Random.value;
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }
}
