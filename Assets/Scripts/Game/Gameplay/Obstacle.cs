using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //O ID do obstáculo deve ser o mesmo da chave do objeto no Inventário
    public int id;
    public GameObject objCollider;

    public void DestroyObstacle()
    {
        //Deixa transparentezinho e desativa o collider pra poder passar
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.3f);
        objCollider.SetActive(false);
    }
}
