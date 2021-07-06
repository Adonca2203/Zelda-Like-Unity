using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidbody;
    public float magicCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUp(Vector2 velocity, Vector3 direction)
    {

        myRigidbody.velocity = velocity.normalized * speed;

        transform.rotation = Quaternion.Euler(direction);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("enemy") && other.isTrigger)
        {

            Destroy(this.gameObject);

        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("room") && other.isTrigger)
        {

            Destroy(this.gameObject);

        }

    }

}
