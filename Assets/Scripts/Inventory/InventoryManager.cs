using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#pragma warning disable CS0649

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject BlankInventorySlot;
    [SerializeField] private GameObject slotParent;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    private InventoryItem currentItem;

    public void SetTextInButton(string desc, bool buttonActive)
    {

        descriptionText.text = desc;

        useButton.SetActive(buttonActive);

    }

    void MakeInventorySlot()
    {

        if(playerInventory != null)
        {

            for(int i = 0; i < playerInventory.myInventory.Count; i++)
            {

                if (playerInventory.myInventory[i].numHeld > 0 || playerInventory.myInventory[i].unique)
                {

                    GameObject temp = Instantiate(BlankInventorySlot, slotParent.transform);

                    ///temp.transform.SetParent(slotParent.transform);

                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                    if (newSlot != null)
                    {

                        newSlot.SetUp(playerInventory.myInventory[i], this);

                    }

                }

            }

        }

    }

    // Start is called before the first frame update
    void OnEnable()
    {

        ClearInvSlots();
        MakeInventorySlot();
        SetTextInButton("", false);

    }

    public void SetUpDescriptionaAndButton(InventoryItem item)
    {

        currentItem = item;

        descriptionText.text = item.itemDesc;

        useButton.SetActive(item.usable);

    }

    void ClearInvSlots()
    {

        for(int i = 0; i < slotParent.transform.childCount; i++)
        {

            Destroy(slotParent.transform.GetChild(i).gameObject);

        }

    }

    public void UseButtonPress()
    {

        if(currentItem)
        {

            currentItem.Use();

            ClearInvSlots();

            MakeInventorySlot();

            SetTextInButton("", false);

        }

    }

}
