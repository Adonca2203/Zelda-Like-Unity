using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{

    [Header("Contents")]
    public PlayerMovement playerMovement;
    public Inventory playerInventory;
    public Item contents;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals & Animations")]
    public Signals raiseItem;
    public GameObject dialogWindow;
    public Text dialogText;

    [Header("Animations")]
    private Animator anim;

    private void OnEnable()
    {
        isOpen = storedOpen.RunTimeValue;
        anim = GetComponent<Animator>();

        if (isOpen)
        {

            anim.SetBool("isOpen", true);

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        if(isOpen)
        {

            anim.SetBool("isOpen", true);

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("interact") && playerInRange)
        {

            if (!isOpen)
            {

                anim.SetBool("opened", true);
                OpenChest();

            }
            else
            {

                ChestIsOpen();

            }

        }

    }

    public void OpenChest()
    {

        if(!this.anim.GetCurrentAnimatorStateInfo(0).IsName("bigOpenIdle"))
        {

            StartCoroutine(WaitForAnimation());

        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("bigOpenIdle"))
        {
            dialogWindow.SetActive(true);
            dialogText.text = contents.itemDescription;
            playerInventory.AddItem(contents);
            playerInventory.currentItem = contents;
            raiseItem.Raise();
            context.Raise();
            isOpen = true;
            storedOpen.RunTimeValue = isOpen;
        }

    }

    private IEnumerator WaitForAnimation()
    {

        playerMovement.currentState = PlayerState.interact;
        float time_to_wait = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(time_to_wait);

        playerMovement.currentState = PlayerState.idle;

        OpenChest();

    }

    public void ChestIsOpen()
    {

        

        dialogWindow.SetActive(false);
        playerInRange = false;
        raiseItem.Raise();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {

            context.Raise();
            playerInRange = true;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {

            context.Raise();
            playerInRange = false;

        }

    }
    
}