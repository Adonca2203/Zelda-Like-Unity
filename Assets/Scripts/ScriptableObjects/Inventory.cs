using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numOfKeys;
    public int coins;
    public FloatValue maxMagic;
    public float currentMagic;

    public void OnEnable()
    {

        currentMagic = maxMagic.RunTimeValue;

    }

    public void ReduceMagic(float magicCost)
    {

        currentMagic -= magicCost;

    }

    public void IncreaseMagic(float magicCost)
    {

        currentMagic += magicCost;

    }

    public bool CheckForItem(Item itemToCheck)
    {

        if(items.Contains(itemToCheck))
        {

            return true;

        }

        else { return false; }

    }

    public void AddItem(Item itemToAdd)
    {

        if(itemToAdd.isKey)
        {

            numOfKeys++;

        }
        else
        {

            if(!items.Contains(itemToAdd))
            {

                items.Add(itemToAdd);

            }

        }

    }

}
