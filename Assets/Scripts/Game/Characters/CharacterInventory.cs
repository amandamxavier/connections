using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public void SetItem(string _key, string _value)
    {
        if (!PlayerPrefs.HasKey(_key))
        {
            PlayerPrefs.SetString(_key, _value);
            Debug.Log("Stored " + _value + " in inventory");
        }
    }

    public void GetItem(string _key, string _value)
    {
        if (PlayerPrefs.HasKey(_key))
        {
            PlayerPrefs.GetString(_key, _value);
        }
    }
}
