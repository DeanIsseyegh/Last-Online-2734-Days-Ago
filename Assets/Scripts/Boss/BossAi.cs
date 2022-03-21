using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    [SerializeField] private GameObject projectile1;
    [SerializeField] private GameObject projectile2;
    [SerializeField] private GameObject projectile3;
    [SerializeField] private GameObject topThrowPoint;
    [SerializeField] private GameObject bottomThrowPoint;
    [SerializeField] private float throwFrequency = 1.5f;
    [SerializeField] private float throwTimer = 1.5f;

    private GameObject lastThrowPoint;
    private int bossPhase = 0;

    void Start()
    {
        lastThrowPoint = topThrowPoint;
    }

    // Update is called once per frame
    void Update()
    {
        throwTimer -= Time.deltaTime;
        if (throwTimer <= 0)
        {
            if (lastThrowPoint == topThrowPoint)
            {
                lastThrowPoint = bottomThrowPoint;
            }
            else
            {
                lastThrowPoint = topThrowPoint;
            }

            var projectileToThrow = DecideProjectile();
            GameObject thrownProjectile =
                Instantiate(projectileToThrow, lastThrowPoint.transform.position, Quaternion.identity);
            Destroy(thrownProjectile, 2.5f);
            throwTimer = throwFrequency;
        }
    }

    private GameObject DecideProjectile()
    {
        if (bossPhase == 0)
        {
            return projectile1;
        }
        if (bossPhase == 1)
        {
            return projectile2;
        }
        return projectile3;
    }

    public void IncreaseBossPhase()
    {
        throwFrequency /= 2;
        bossPhase++;
    }
}