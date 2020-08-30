using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{

    public static InventoryHUD instance;
    private void Awake() {
        instance = this;
    }

    public Sprite[] sprItens;
    public GameObject itemPrefab;

    public void AddItem(int id)
    {
        //Instancia um novo objeto no HorizontalLayout do HUD
        GameObject newItem = Instantiate(itemPrefab);

        //Faz o set do parent, da sprite e do tamanho
        newItem.transform.SetParent(gameObject.transform, false);
        //Sabe a sprite através da key do PlayerPrefs que começa com 1 e aqui tem que ser 0
        newItem.GetComponent<Image>().sprite = sprItens[id - 1];
        newItem.GetComponent<Image>().SetNativeSize();
    }
}
