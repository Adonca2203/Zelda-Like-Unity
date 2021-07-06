using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{

    public Collider2D boundary;

    public override void CheckDistance()
    {

        if ((Vector3.Distance(transform.position, target.position) <= chaseRadius)
            && (Vector3.Distance(transform.position, target.position) > attackRadius)
            && (enemyState == EnemyState.idle || enemyState == EnemyState.walk)
            && (enemyState != EnemyState.stagger)
            && boundary.bounds.Contains(target.transform.position))
            {

               Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);

            }

        else if ((Vector3.Distance(transform.position, target.position) > chaseRadius || !boundary.bounds.Contains(target.transform.position)))
        {

            if (homePosition != null)
            { WalkHome(); }

            else
            { anim.SetBool("wakeUp", false); }

        }

    }

}
