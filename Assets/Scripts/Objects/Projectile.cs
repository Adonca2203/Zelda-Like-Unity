using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Movement")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Projectile Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;

    }

    // Update is called once per frame
    void Update()
    {

        lifetimeSeconds -= Time.deltaTime; //Destroy after lifetimeSeconds have passed
        if(lifetimeSeconds <= 0)
        {

            Destroy(this.gameObject);

        }

    }

    public void Launch(Vector2 initialVel)
    {

        myRigidbody.velocity = initialVel * moveSpeed; //Projectile travel

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("room"))
        {

            Destroy(this.gameObject);
        
        }

    }

}
