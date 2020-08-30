using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    //[ID] - [Item] x [Obstáculo]
    //[1] - [Carta] x [Ansiedade]
    //[2] - [Celular] x [Porta 1] [Porta 2]
    //[3] - [Corda] x [Janela]
    //[4] - [Patins] x [Janela]

    public InventoryHUD hud;

    void Start()
    {
        //Começa limpando todo o PlayerPrefs para não ter nada salvo
        PlayerPrefs.DeleteAll();
    }

    public void SetItem(string _key, string _value)
    {
        //Se já não existe, cria um novo item
        if (!PlayerPrefs.HasKey(_key))
        {
            PlayerPrefs.SetString(_key, _value);
            //Passa para o HUD com a chave do item
            hud.AddItem(int.Parse(_key));
        }
    }

    //Retorna se tem o item através da chave
    public bool GetItem(string _key)
    {
        return PlayerPrefs.HasKey(_key);
    }
}
