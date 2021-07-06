using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{

    idle,
    walk,
    attack,
    interact,
    stagger,
    indoors
        
}

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Stats")]
    public float speed;
    public PlayerState currentState;
    public SecondaryState secondaryState;
    public SpriteRenderer receivedItemSprite;
    private Rigidbody2D myRigidbody;
    private Animator animator;

    [Header("Player Position")]
    public VectorValue startingPosition;
    private Vector3 change;

    // TO DO: HEALTH break health system into its own component
    public FloatValue currentHealth;
    public Signals playerHealthSignal;

    // TO DO: break inventory into its own component
    public Inventory playerInventory;

    // TO DO: HEALTH screenKick with health system?
    public Signals screenKick;
    
    // TO DO: updateMagic should be part of the magic system
    public Signals updateMagic;


    // TO DO: break off the iFrame Stuff
    [Header("iFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;
    
    //TO DO: break off projectile stuff
    [Header("Projectile Stuff")]
    public GameObject projectile;
    public Item bow;

    // Start is called before the first frame update
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        currentState = PlayerState.idle;

        transform.position = startingPosition.initialValue;

    }

    // Update is called once per frame
    void Update()
    {

        if (currentState == PlayerState.interact)
        {

            animator.SetBool("moving", false);
            return;

        }

        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger && secondaryState.indoors != true)
        {

            StartCoroutine(AttackCo());

        }

        else if(Input.GetButtonDown("secondary weapon") && currentState != PlayerState.attack
                && currentState != PlayerState.stagger && secondaryState.indoors != true)
        {

            if (playerInventory.CheckForItem(bow))
            {

                StartCoroutine(SecondAttackCo());

            }

        }

        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {

            UpdateAnimationAndMove();

        }

    }

    private IEnumerator AttackCo()
    {

        animator.SetBool("attacking", true);
        ChangeState(PlayerState.attack);

        yield return null;

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);

        ChangeState(PlayerState.idle);

    }

    private IEnumerator SecondAttackCo()
    {

        //animator.SetBool("attacking", true);
        ChangeState(PlayerState.attack);

        yield return null;

        MakeArrow();

        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);

        ChangeState(PlayerState.idle);

    }

    // TO DO: This should be part of the ability itself
    private void MakeArrow()
    {

        if (playerInventory.currentMagic > 0)
        {

            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));

            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.SetUp(temp, ChooseArrowDirection());

            playerInventory.ReduceMagic(arrow.magicCost);
            updateMagic.Raise();

        }

    }

    // TO DO: This should be part of the ability itself
    private Vector3 ChooseArrowDirection()
    {

        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);

    }

    public void RaiseItem()
    {

        if (playerInventory.currentItem != null)
        {

            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {

                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;


            }
        
        }
    }

    void UpdateAnimationAndMove()
    {

        if (change != Vector3.zero)
        {

            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);

        }

        else
        {

            animator.SetBool("moving", false);

        }

    }

    void MoveCharacter()
    {

        change.Normalize();

        myRigidbody.MovePosition(

            transform.position + change * speed * Time.deltaTime

            );

    }

    public void ChangeState(PlayerState newState)
    {

        if(currentState != newState)
        {

            currentState = newState;

        }

    }

    // TO DO: move knock to its own scripts
    public void Knock(float knockTime, float damage)
    {

        currentHealth.RunTimeValue -= (damage);

        if (currentHealth.RunTimeValue > 0)
        {

            //TO DO: HEALTH
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));

        }else
        {

            this.gameObject.SetActive(false);

        }  

    }

    // TO DO: move knockco to its own scripts
    private IEnumerator KnockCo(float knockTime)
    {

        screenKick.Raise();

        if (myRigidbody != null)
        {

            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            ChangeState(PlayerState.idle);
            myRigidbody.velocity = Vector2.zero;

        }

    }

    // TO DO: move flashco to its own scripts
    private IEnumerator FlashCo()
    {

        int temp = 0;
        triggerCollider.enabled = false;

        while(temp < numberOfFlashes)
        {

            mySprite.color = flashColor;

            yield return new WaitForSeconds(flashDuration);

            mySprite.color = regularColor;

            yield return new WaitForSeconds(flashDuration);

            temp++;

        }

        triggerCollider.enabled = true;

    }

}
