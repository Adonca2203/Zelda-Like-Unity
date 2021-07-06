using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) //Break breakable objects when hit
        {

            other.GetComponent<Pot>().Smash(); //only have a pot right now
            return; //Skip knockback and damage calculations

        }

        else if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player")) //Player or enemy was hit
        {

            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {

                //Calculate and apply knockback to the thing that was hit
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                //Collides with enemy hurtbox
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {

                    EnemyHit(other, hit);

                }

                //Collides with player hurtbox
                if (other.gameObject.CompareTag("Player") && other.isTrigger)
                {

                    PlayerHit(other, hit);

                }

            }

        }

    }
    
    private void EnemyHit(Collider2D enemyCollider, Rigidbody2D enemyRigidbody)
    {

        if (this.gameObject.CompareTag("enemy")) //Enemy attacks enemy, no damage dealt, stagger the enemy
        {

            enemyRigidbody.GetComponent<EnemyAI>().enemyState = EnemyState.stagger;
            enemyCollider.GetComponent<EnemyAI>().Knock(enemyRigidbody, knockTime, 0);

        }

        else //Player attacks enemy, deal damage and stagger the enemy
        {

            enemyRigidbody.GetComponent<EnemyAI>().enemyState = EnemyState.stagger;
            enemyCollider.GetComponent<EnemyAI>().Knock(enemyRigidbody, knockTime, damage);

        }

    }

    private void PlayerHit(Collider2D playerCollider, Rigidbody2D playerRigidbody)
    {

        if (playerCollider.GetComponent<PlayerMovement>().currentState != PlayerState.stagger) //damage and stagger the player
        {

            playerRigidbody.GetComponent<PlayerMovement>().ChangeState(PlayerState.stagger);
            playerCollider.GetComponent<PlayerMovement>().Knock(knockTime, damage);

        }

    }

}
