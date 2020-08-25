using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Essa classe é responsável por enviar e receber as informações de sincronização do servidor
public class CharacterNetwork : MonoBehaviourPunCallbacks
{

    public SpriteRenderer spriteRenderer;
    public Sprite player1Sprite;
    public Sprite player2Sprite;
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

            if (PhotonNetwork.MasterClient == photonView.Owner) {
                spriteRenderer.sprite = player1Sprite;//Change to setting animator instead of a static sprite
            }
            else {
                spriteRenderer.sprite = player2Sprite;
            }
        }
    }

}
