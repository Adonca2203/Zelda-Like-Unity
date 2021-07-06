using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{

    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue heartContainers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player") && !other.isTrigger)
        {

            playerHealth.RunTimeValue += amountToIncrease;

            if(playerHealth.RunTimeValue > heartContainers.RunTimeValue * 2)
            {

                playerHealth.RunTimeValue = heartContainers.RunTimeValue * 2;

            }

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
