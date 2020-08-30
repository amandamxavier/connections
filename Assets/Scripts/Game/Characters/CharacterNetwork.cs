using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Essa classe é responsável por enviar e receber as informações de sincronização do servidor
public class CharacterNetwork : MonoBehaviourPunCallbacks
{

    public Animator p1AnimatedPrefab;
    public Animator p2AnimatedPrefab;
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
                //CameraController.target = transform;
            }

            if (PhotonNetwork.MasterClient == photonView.Owner) {
                Instantiate(p1AnimatedPrefab, transform);
            }
            else {
                Instantiate(p2AnimatedPrefab, transform);
            }
        }
    }

    public override void OnLeftRoom()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().CallDisconnectGame();
    }




}
