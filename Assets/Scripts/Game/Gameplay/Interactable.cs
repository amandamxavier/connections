using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public static Interactable hovered = null;

    [Header("Interactivity")]
    public bool isUsable;

    [Header("PlayerPrefs Info")]
    public string key;
    public string value;

    [Header("Visual Feedback")]
    public GameObject indicator;

    private void Update() {
        if (indicator != null) {
            indicator.SetActive(hovered == this);
        }
    }

    public void CollectObj()
    {
        isUsable = false;
        indicator.GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
