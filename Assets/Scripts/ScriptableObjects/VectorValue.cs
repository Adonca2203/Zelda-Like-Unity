using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{

    [Header("Value currently running")]
    public Vector2 initialValue;
    [Header("Default value")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {

        initialValue = defaultValue;

    }

    public void OnBeforeSerialize()
    {



    }

}
