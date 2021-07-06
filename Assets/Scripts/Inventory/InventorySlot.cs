using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#pragma warning disable CS0649
public class InventorySlot : MonoBehaviour
{
    [Header("UI Changes")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the Item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;
    
    public void SetUp(InventoryItem newItem, InventoryManager newManager)
    {

        thisItem = newItem;
        thisManager = newManager;

        if(thisItem != null)
        {

            itemImage.sprite = thisItem.itemImage;

            if (thisItem.unique && !thisItem.usable)
            { itemNumberText.text = ""; }

            else
            {

                itemNumberText.text = "" + thisItem.numHeld;

            }

        }

    }

    public void ClickedOn()
    {

        if(thisItem)
        {

            thisManager.SetUpDescriptionaAndButton(thisItem);

        }

    }

}
