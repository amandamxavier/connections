using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndGame : MonoBehaviour
{
    public int[] objsNecesarios;
    bool gameEnded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterInventory inventory = collision.gameObject.GetComponent<CharacterInventory>();

            //Se Gabriela tem o celular, envia a localização
            if (inventory.GetItem(objsNecesarios[0].ToString()) && inventory.GetItem(objsNecesarios[1].ToString()) && gameEnded == false)
            {
                Debug.Log("Trigger interaction!");
                Debug.Log("End Game");
                gameEnded = true;
            }
        }
    }
}
