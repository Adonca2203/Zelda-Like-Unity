using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{

    idle,
    walk,
    attack,
    stagger

}

public class EnemyAI : MonoBehaviour
{

    [Header("State Machine")]
    public EnemyState enemyState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public bool canRespawn = true;

    [Header("Animator")]
    public Animator anim;

    [Header("Home Position Variables")]
    public Transform homePosition;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    public LootTable thisLoot;

    [Header("Death Signals")]
    public Signals roomSignal;

    private void Awake()
    {

        health = maxHealth.initialValue;
        enemyState = EnemyState.idle;

    }

    private void OnEnable()
    {

        health = maxHealth.initialValue;
        enemyState = EnemyState.idle;

        if (homePosition != null)
        {

            transform.position = homePosition.position;

        }
        else { }

    }

    private void TakeDamage(float damage)
    {

        health -= damage;

        if (health <= 0)
        {

            DeathEffect();
            MakeLoot();

            if (!canRespawn)
            {

                Destroy(this.gameObject);

            }

            else
            {

                this.gameObject.SetActive(false);

            }

            if (roomSignal != null)
            {
                roomSignal.Raise();
            }

        }

    }

    private void MakeLoot()
    {

        if(thisLoot != null)
        {

            PowerUp current = thisLoot.LootPowerUp();

            if(current != null)
            {

                Instantiate(current.gameObject, transform.position, Quaternion.identity);

            }

            else { return; }

        }

    }
    private void DeathEffect()
    {

        if(deathEffect != null)
        {

            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);

        }

    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {

        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);

    }


    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {

        if (myRigidbody != null)
        {

            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            enemyState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;

        }

    }

    public void WaitForAnimation()
    {

        StartCoroutine(WaitForAnimationCo());

    }

    private IEnumerator WaitForAnimationCo()
    {

        float time_to_wait = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(time_to_wait);

    }


}
