using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InventorySaver : MonoBehaviour
{

    [SerializeField] private PlayerInventory playerInventory;

    private void OnEnable()
    {

        playerInventory.myInventory.Clear();
        LoadScriptables();

    }


    private void OnDisable()
    {
        SaveScriptables();
    }

    public void ResetScriptables()
    {

        int i = 0;

        while (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.inv", i)))
        {

            File.Delete(Application.persistentDataPath +
                string.Format("/{0}.inv", i));

            i++;

        }

    }

    public void SaveScriptables()
    {

        ResetScriptables();

        for (int i = 0; i < playerInventory.myInventory.Count; i++)
        {

            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.inv", i));

            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(playerInventory.myInventory[i]);
            binary.Serialize(file, json);

            file.Close();

        }

    }

    public void LoadScriptables()
    {

        int i = 0;

        while (File.Exists(Application.persistentDataPath +
            string.Format("/{0}.inv", i)))
        {

            var temp = ScriptableObject.CreateInstance<InventoryItem>();

            FileStream file = File.Open(Application.persistentDataPath +
                string.Format("/{0}.inv", i), FileMode.Open);

            BinaryFormatter binary = new BinaryFormatter();

            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                temp);

            file.Close();

            playerInventory.myInventory.Add(temp);

            i++;

        }


    }
}
