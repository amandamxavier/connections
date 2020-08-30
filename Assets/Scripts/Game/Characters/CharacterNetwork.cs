using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Essa classe é responsável por enviar e receber as informações de sincronização do servidor
public class CharacterNetwork : MonoBehaviourPunCallbacks
{

    public Animator p1Animator;
    public Animator p2Animator;
    public MonoBehaviour[] playerScripts;

    void Start()
    {
        Initialize();
    }

    //Configurações iniciais do PhotonView para diferenciar os objetos
    void Initialize()
    {
        if(photonView != null)
        {
            //Uma "gambiarra" para desativar todos os scripts de controle do objeto player se este não for o do client
            if (!photonView.IsMine)
            {
                foreach(MonoBehaviour mb in playerScripts)
                {
                    mb.enabled = false;
                }
            }
            else {
                CameraController.target = transform;
            }

            if (PhotonNetwork.MasterClient == photonView.Owner) {
                p1Animator.gameObject.SetActive(true);
                p2Animator.gameObject.SetActive(false);
            }
            else {
                p1Animator.gameObject.SetActive(false);
                p2Animator.gameObject.SetActive(true);
            }
        }
    }

    public override void OnLeftRoom()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().CallDisconnectGame();
    }




}
