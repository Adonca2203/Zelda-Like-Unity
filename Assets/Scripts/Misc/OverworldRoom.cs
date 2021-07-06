using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldRoom : Room
{

    public TreasureChest[] chests;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if(other.CompareTag("Player") && !other.isTrigger)
        {

            for(int i = 0; i < chests.Length; i++)
            {

                ChangeActivation(chests[i], true);

            }

        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.CompareTag("Player") && !other.isTrigger)
        {

            for (int i = 0; i < chests.Length; i++)
            {

                ChangeActivation(chests[i], false);

            }

        }

    }

}
