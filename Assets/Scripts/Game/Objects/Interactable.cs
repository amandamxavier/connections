using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactivity")]
    public bool onReach;
    public bool isUsable;

    [Header("PlayerPrefs Info")]
    public string key;
    public string value;

    [Header("Visual Feedback")]
    public GameObject indicator;
    public LayerMask playerLayer;
    public float activationRadius;

    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, activationRadius, playerLayer))
        {
            ShowIndicator();
            onReach = true;
        } else
        {
            HideIndicator();
            onReach = false;
        }
    }

    void ShowIndicator()
    {
        indicator.gameObject.SetActive(true);
    }

    void HideIndicator()
    {
        indicator.gameObject.SetActive(false);
    }

    public void CollectObj()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
                        
    }
}
