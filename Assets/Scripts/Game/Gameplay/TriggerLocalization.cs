using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriggerLocalization : MonoBehaviourPun
{
    public int objNecessario;

    public bool localizationSent = false;

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
                photonView.RPC("Send", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void Send() {
        localizationSent = true;
    }
}
