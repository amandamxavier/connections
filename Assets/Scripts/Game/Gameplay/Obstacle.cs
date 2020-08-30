using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviourPun
{
    //O ID do obstáculo deve ser o mesmo da chave do objeto no Inventário
    public int id;
    public GameObject objCollider;

    public void DestroyObstacle() {
        photonView.RPC("RPCDestroyObstacle", RpcTarget.All);
    }
    [PunRPC]
    public void RPCDestroyObstacle() {
        //Deixa transparentezinho e desativa o collider pra poder passar
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.3f);
        objCollider.SetActive(false);
    }

}
