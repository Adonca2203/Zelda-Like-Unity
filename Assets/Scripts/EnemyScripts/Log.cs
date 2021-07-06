using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : EnemyAI
{

    public Rigidbody2D myRigidbody;

    [Header("Target Variable")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyState = EnemyState.idle;
        anim.SetBool("wakeUp", true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        CheckDistance();

    }

    public virtual void CheckDistance()
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
            anim.SetBool("wakeUp", true);

        }
        else if ((Vector3.Distance(transform.position, target.position) > chaseRadius))
        {

            if(homePosition != null)
            { WalkHome(); }

            else
            { anim.SetBool("wakeUp", false); }
            
        }

    }
    private void SetAnimFloat(Vector2 setVecor)
    {

        anim.SetFloat("moveX", setVecor.x);
        anim.SetFloat("moveY", setVecor.y);

    }
    public void ChangeAnim(Vector2 direction)
    {

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {

            if (direction.x > 0)
            {

                SetAnimFloat(Vector2.right);

            } else if (direction.x <0)
            {

                SetAnimFloat(Vector2.left);

            }

        } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {

            if (direction.y > 0)
            {

                SetAnimFloat(Vector2.up);

            } else if (direction.y < 0)
            {

                SetAnimFloat(Vector2.down);

            }

        }

    }

    public void ChangeState(EnemyState newState)
    {

        if(enemyState != newState)
        {

            enemyState = newState;

        }

    }

    public void WalkHome()
    {

        Vector3 homeTemp = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
        ChangeAnim(homeTemp - transform.position);
        myRigidbody.MovePosition(homeTemp);

        if (homeTemp - transform.position == Vector3.zero)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("wakeUp", false);
        }

    }

}
