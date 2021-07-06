using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{

    public Inventory playerInventory;
    public Signals magicSignal;

    public void Use(int amountToIncrease)
    {

        playerInventory.IncreaseMagic(amountToIncrease);

        magicSignal.Raise();

    }

}
