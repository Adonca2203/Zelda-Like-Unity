using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking.Types;

public class GameSaveManager : MonoBehaviour
{

    public static GameSaveManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    private bool reset = false;

    public void ResetScriptables()
    {

        for(int i = 0; i < objects.Count; i++)
        {

            if(File.Exists(Application.persistentDataPath +
                    string.Format("/{0}.dat", i)))
            {

                File.Delete(Application.persistentDataPath +
                    string.Format("/{0}.dat", i));

            }

        }

        reset = true;

    }

    private void OnEnable()
    {
        LoadScriptables();
    }


    private void OnDisable()
    {
        if(reset)
        {

            reset = false;
            return;

        }

        SaveScriptables();
    }

    public void SaveScriptables()
    {

        for(int i = 0; i < objects.Count; i++)
        {

            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.dat", i));

            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);

            file.Close();

        }

    }

    public void LoadScriptables()
    {

        for(int i = 0; i < objects.Count; i++)
        {

            if(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {

                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/{0}.dat", i), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                    objects[i]);

                file.Close();

            }

        }

    }

}