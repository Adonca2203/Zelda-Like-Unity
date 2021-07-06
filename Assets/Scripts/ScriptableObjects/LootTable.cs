using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Loot //A loot object; A powerup with a probability to drop
{

    public PowerUp thisLoot;
    public float lootChance;

}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{

    public Loot[] loots; //A list of loot objects; A table of loot

    public PowerUp LootPowerUp() //Get a loot from the loot table
    {

        float cumulativeProbability = 0f; //starting probability
        float currentProbability = Random.Range(0, 100); //random number to test into probability

        for (int i = 0; i < loots.Length; i++)
        {

            cumulativeProbability += loots[i].lootChance; //Probability of obtaining this item

            if(currentProbability <= cumulativeProbability) //Is your number less than or equal to this probability
            {

                return loots[i].thisLoot; //Return the item;

            }

        }

        return null; //Return nothing if your probability does not match the probability of any items

    }

}
