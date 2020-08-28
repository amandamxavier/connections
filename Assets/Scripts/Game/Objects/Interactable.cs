using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactivity")]
    public bool isUsable;

    [Header("PlayerPrefs Info")]
    public string key;
    public string value;

    [Header("Visual Feedback")]
    public GameObject indicator;
    //public float activationRadius;

    private void OnMouseOver()
    {
        indicator.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        indicator.gameObject.SetActive(false);
    }

    public void CollectObj()
    {
        isUsable = false;
        indicator.GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
