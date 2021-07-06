using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Sign
{

    private Vector3 direction;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRidigbody;
    private Animator Anim;
    public Collider2D bounds;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {

        myTransform = GetComponent<Transform>();
        myRidigbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);

        ChangeDirection();

    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
        if(isMoving)
        {

            moveTimeSeconds -= Time.deltaTime;

            if(moveTimeSeconds <= 0)
            {

                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;

            }

            if (!playerInRange)
            {

                Move();

            }

        }

        else
        {

            waitTimeSeconds -= Time.deltaTime;

            if(waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();

                isMoving = true;

                waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);

            }

        }
        
    }

    private void ChooseDifferentDirection()
    {

        Vector3 temp = direction;

        int loops = 0;

        while (temp == direction && loops < 100)
        {

            ChangeDirection();
            loops++;

        }

    }

    private void Move()
    {

        Vector3 temp = myTransform.position + direction * speed * Time.deltaTime;

        if (bounds.bounds.Contains(temp))
        {

            myRidigbody.MovePosition(temp);

        }
        else
        {

            ChangeDirection();

        }

    }

    void UpdateAnimation()
    {

        Anim.SetFloat("moveX", direction.x);
        Anim.SetFloat("moveY", direction.y);

    }

    void ChangeDirection()
    {

        int pickDirection = Random.Range(0, 4);

        switch(pickDirection)
        {

            case 0: //Right

                direction = Vector3.right;

                break;

            case 1: //Up

                direction = Vector3.up;

                break;

            case 2: //Left

                direction = Vector3.left;

                break;

            case 3: //Down

                direction = Vector3.down;

                break;

            default:

                break;

        }

        UpdateAnimation();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        ChooseDifferentDirection();

    }

}
