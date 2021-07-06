using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{

    public PlayerMovement playerMovement;
    public GameObject DialogBox;
    public Text DialogText;
    public string dialog;

    void Start()
    {
        
    }

    public virtual void Update()
    {

        if (Input.GetButtonDown("interact") && playerInRange)
        {

            if (DialogBox.activeInHierarchy)
            {

                DialogBox.SetActive(false);
                playerMovement.ChangeState(PlayerState.idle);

            }

            else
            {

                DialogBox.SetActive(true);
                playerMovement.ChangeState(PlayerState.interact);
                DialogText.text = dialog;

            }

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {

            context.Raise();
            playerInRange = false;
            DialogBox.SetActive(false);

        }

    }

}
