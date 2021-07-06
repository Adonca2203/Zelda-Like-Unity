using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{

    key,
    enemy,
    button

}

public class Door : Interactable
{

    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private void Update()
    {
        
        if(Input.GetButtonDown("interact"))
        {

            if(playerInRange && thisDoorType == DoorType.key)
            {

                //does the player have a key?
                if(playerInventory.numOfKeys > 0)
                {

                    playerInventory.numOfKeys--;
                    Open();
                

                }


            }

        }
    }

    public void Open()
    {

        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;

    }

    public void Close()
    {

        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;

    }

}
