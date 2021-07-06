using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : PatrolLog
{

    private float attack_animation_length = .2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CheckDistance()
    {

        if (path[currentPoint] != null)
        {

            homePosition = path[currentPoint];

        }

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

        else if ((Vector3.Distance(transform.position, target.position) < attackRadius))
        {
            if ((enemyState == EnemyState.idle || enemyState == EnemyState.walk)
                && (enemyState != EnemyState.stagger))
            {
                StartCoroutine(AttackCo());
            }

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

    /*    public override void CheckDistance()
        {

            if ((Vector3.Distance(transform.position, target.position) <= chaseRadius)
                && (Vector3.Distance(transform.position, target.position) > attackRadius)
                && (enemyState == EnemyState.idle || enemyState == EnemyState.walk)
                && (enemyState != EnemyState.stagger))
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);

            }

            else if ((Vector3.Distance(transform.position, target.position) > chaseRadius))
            {

                if (homePosition != null)
                { WalkHome(); }

                else
                { }

            }

        }
        */

    public IEnumerator AttackCo()
    {

        enemyState = EnemyState.attack;

        anim.SetBool("attack", true);

        yield return new WaitForSeconds(attack_animation_length);

        anim.SetBool("attack", false);
        enemyState = EnemyState.walk;

    }

}
