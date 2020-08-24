using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Essa classe é responsável por enviar e receber as informações de sincronização do servidor
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

    //Configurações iniciais do PhotonView para diferenciar os objetos
    void Initialize()
    {
        if(pView != null)
        {
            //Uma "gambiarra" para desativar todos os scripts de controle do objeto player se este não for o do client
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

    //Função da classe do Photon que manda e recebe as informações através do servidor
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Se está mandando informações
        if (stream.IsWriting)
        {
            //Envia as informações de posição e rotação
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
