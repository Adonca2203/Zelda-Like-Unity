using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{

    [Header("Projectile")]
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {

        fireDelaySeconds -= Time.deltaTime;

        if(fireDelaySeconds <= 0)
        {

            canFire = true;
            fireDelaySeconds = fireDelay;

        }

    }

    public override void CheckDistance()
    {

        if ((Vector3.Distance(transform.position, target.position) <= chaseRadius)
            && (Vector3.Distance(transform.position, target.position) > attackRadius)
            && (enemyState == EnemyState.idle || enemyState == EnemyState.walk)
            && (enemyState != EnemyState.stagger))
        {

            if (canFire)
            {

                Vector3 tempVector = target.transform.position - transform.position;
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector);
                canFire = false;
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);

            }

        }
        else if ((Vector3.Distance(transform.position, target.position) > chaseRadius))
        {

            if (homePosition != null)
            { WalkHome(); }

            else
            { anim.SetBool("wakeUp", false); }

        }

    }

}
