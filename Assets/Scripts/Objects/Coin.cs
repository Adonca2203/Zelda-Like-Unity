using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{

    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        powerUpSignal.Raise();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {

            playerInventory.coins += 1;
            powerUpSignal.Raise();

            if (canRespawn)
            {

                this.gameObject.SetActive(false);

            }
            else
            {

                Destroy(this.gameObject);

            }

        }

    }

}
