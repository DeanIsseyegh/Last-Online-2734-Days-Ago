using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float failTimer = 10f;//10 seconds
    [SerializeField] Text failTimertext, winText, loseText;
    bool pause = false;

    private void Start()
    {
        Resize();
    }
    void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = ls;

    }

    private void Update()
    {
        if (pause)
            return;

        Fish();
        Hook();
        ProgressCheck();
    }
    void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;//local scale
        ls.y = hookProgress;//set the y scale to the hook progress
        progressBarContainer.localScale = ls;//set the local scale

        float min = hookPosition - hookSize / 2f;//get the minimum position
        float max = hookPosition + hookSize / 2f;//get the maximum position

        if (min < fishPosition && max > fishPosition)
        {//if the fish is in the hook
            hookProgress += hookPower * Time.deltaTime;//increase the hook progress
            failTimertext.text = "Fail Time:" + failTimer.ToString("0.0");//set the fail timer text

        }
        else
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;//decrease the hook progress
            failTimer -= Time.deltaTime;//decrease the fail timer
            failTimertext.text = "Fail Time:" + failTimer.ToString("0.0");//set the fail timer text
            if (failTimer <= 0)
            {
                pause = true;
                print("fail");
                loseText.gameObject.SetActive(true);
            }
        }
        if (hookProgress >= 1)
        {
            Win();
            print("win");
            winText.gameObject.SetActive(true);
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);//clamp the hook progress  
    }

    private void Win()
    {
        pause = true;
    }

    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;//increase the hook pull velocity
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;//decrease the hook pull velocity

        hookPosition += hookPullVelocity;//move the hook
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);//clamp the hook position
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);//move the hook

        if (hookPosition - hookSize / 2 <= 0f && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }
        if (hookPosition + hookSize / 2 >= 1f && hookPullVelocity > 0f)
        {
            hookPullVelocity = 0f;
        }
    }

    private void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplier;//set the fish timer
            fishDestination = UnityEngine.Random.value;//get a random destination
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);//move the fish
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);//move the fish
    }
}
