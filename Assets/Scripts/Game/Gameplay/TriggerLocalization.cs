﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLocalization : MonoBehaviour
{
    public int objNecessario;

    bool localizationSent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterInventory inventory = collision.gameObject.GetComponent<CharacterInventory>();
            
            //Se Gabriela tem o celular, envia a localização
            if (inventory.GetItem(objNecessario.ToString()) && localizationSent == false)
            {
                Debug.Log("Trigger interaction!");
                Debug.Log("Send localization");
                localizationSent = true;
            }
        }
    }
}
