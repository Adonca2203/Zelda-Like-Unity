using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PhysicalInventoryItem : MonoBehaviour
{

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player") && !other.isTrigger)
        {

            AddItemToInventory();
            Destroy(this.gameObject);

        }

    }

    void AddItemToInventory()
    {

        if(playerInventory && thisItem)
        {

            if(playerInventory.myInventory.Contains(thisItem))
            {

                thisItem.numHeld++;

            }

            else
            {

                playerInventory.myInventory.Add(thisItem);
                thisItem.numHeld += 1;

            }

        }

    }

}
