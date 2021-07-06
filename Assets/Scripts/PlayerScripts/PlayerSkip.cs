using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkip : MonoBehaviour
{

    public Vector3 distanceToSkip;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player") && !other.isTrigger)
        {

            other.transform.position += distanceToSkip;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if(other.CompareTag("Player") && !other.isTrigger)
        {

            this.gameObject.SetActive(false);

        }

    }

}
