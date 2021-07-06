using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    public override void CheckDistance()
    {

        if (path[currentPoint] != null)
        {

            homePosition = path[currentPoint];

        }

        anim.SetBool("wakeUp", true);

        if ((Vector3.Distance(transform.position, target.position) <= chaseRadius)
            && (Vector3.Distance(transform.position, target.position) > attackRadius)
            && (enemyState == EnemyState.idle || enemyState == EnemyState.walk)
            && (enemyState != EnemyState.stagger))
        {

            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            ChangeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            //ChangeState(EnemyState.walk);

        }
        else if ((Vector3.Distance(transform.position, target.position) > chaseRadius))
        {
            if (Vector3.Distance(transform.position,
                                path[currentPoint].position) > roundingDistance)
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position,
                                                    moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

            }

            else
            {

                ChangeGoal();

            }

        }


    }

    public void ChangeGoal()
    {

        if(currentPoint == path.Length - 1)
        {

            currentPoint = 0;
            currentGoal = path[0];

        }
        else
        {

            currentPoint += 1;
            currentGoal = path[currentPoint];

        }

    }

}
