using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WatingRoomManager : MonoBehaviourPunCallbacks
{
    public Text userMessage, pingText;
    public GameObject goBtn;
    public GameObject backBtn;

    bool deuErro;

    private void Start()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        goBtn.SetActive(false);

        //PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        //Se já há duas pessoas na sala, inicia o jogo
        if(!deuErro && PhotonNetwork.PlayerList.Length == 2)
        {
            userMessage.text = "Game is ready";

            //Apenas quem criou a sala pode dar GO no jogo
            if (PhotonNetwork.IsMasterClient)
            {
                goBtn.SetActive(true);
            }

            //goBtn.SetActive(true);

        } else if (!deuErro)
        {
            userMessage.text = "Waiting...";
        }

        pingText.text = PhotonNetwork.GetPing().ToString();
    }

    //Callback de erro ao se juntar a sala
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        deuErro = true;
        userMessage.text = "This room is full. Please try another one";
    }

    public void BackToMenu()
    {
        PhotonNetwork.LoadLevel("Menu");
        PhotonNetwork.LeaveRoom();
    }

    //Passa a função de carregar a cena via RPC para todos os clientes da sala
    public void CallLoadGameScene()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("LoadGameScene", RpcTarget.All);
    }

    //Função RPC que carrega a cena
    [PunRPC]
    public void LoadGameScene()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
