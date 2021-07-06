using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{

    public string itemName;
    public string itemDesc;
    public Sprite itemImage;
    public int numHeld;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;

    public void Use()
    {

        thisEvent.Invoke();

    }

    public void DecreaseAmount(int amountToDecrease)
    {

        numHeld -= amountToDecrease;

        if(numHeld < 0)
        {

            numHeld = 0;

        }

    }

}
