using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBottle : PowerUp
{

    public float magicCost;
    public Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            playerInventory.IncreaseMagic(magicCost);

            powerUpSignal.Raise();

            if (!canRespawn)
            {

                Destroy(this.gameObject);

            }

            else
            {

                this.gameObject.SetActive(false);

            }

        }
    }
}
