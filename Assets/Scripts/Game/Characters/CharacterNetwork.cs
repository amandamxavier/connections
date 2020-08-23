using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterNetwork : MonoBehaviourPunCallbacks
{
    PhotonView pView;
    Vector3 realPos = Vector3.zero;
    Quaternion realRot = Quaternion.identity;

    public MonoBehaviour[] playerScripts;

    void Start()
    {
        pView = GetComponent<PhotonView>();
        Initialize();
    }

    void Initialize()
    {
        if(pView != null)
        {
            if (!pView.IsMine)
            {
                foreach(MonoBehaviour mb in playerScripts)
                {
                    mb.enabled = false;
                }

                //Se não sou eu, é o quadrado verde
                GetComponent<SpriteRenderer>().color = new Color(0, 153, 51);
            } else
            {
                //Se sou eu, é o quadrado rosa
                GetComponent<SpriteRenderer>().color = new Color(204, 0, 102);
            }
        }
    }

    //Função da classe do Photon que manda e recebe as informações
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Se está mandando informações
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Se está recebendo informações
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
