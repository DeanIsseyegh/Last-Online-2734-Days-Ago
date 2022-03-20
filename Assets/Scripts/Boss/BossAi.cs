using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject topThrowPoint;
    [SerializeField] private GameObject bottomThrowPoint;
    [SerializeField] private float throwFrequency = 1.5f;
    [SerializeField] private float throwTimer = 1.5f;

    private GameObject lastThrowPoint;
    
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
            GameObject thrownProjectile = Instantiate(projectile, lastThrowPoint.transform.position, Quaternion.identity);
            Destroy(thrownProjectile, 2.5f);
            throwTimer = throwFrequency;
        }
    }

    public void IncreaseThrowFrequency()
    {
        throwFrequency /= 2;
    }
}
