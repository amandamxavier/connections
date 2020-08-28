using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    //[ID] - [Item] x [Obstáculo]
    //[1] - [Carta] x [Ansiedade]

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetItem(string _key, string _value)
    {
        if (!PlayerPrefs.HasKey(_key))
        {
            PlayerPrefs.SetString(_key, _value);
            Debug.Log("Stored " + _value + " in inventory");
        }
    }

    public bool GetItem(string _key)
    {
        return PlayerPrefs.HasKey(_key);
    }
}
