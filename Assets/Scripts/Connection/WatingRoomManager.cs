using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WatingRoomManager : MonoBehaviourPunCallbacks
{
    public GameObject readyObj;
    public Text pingText;
    public GameObject goBtn;
    public GameObject backBtn;
    public string menuSceneName = "Menu";
    public string gameSceneName = "Chapter1";

    bool deuErro;

    private void Start()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        goBtn.SetActive(false);
    }

    void Update()
    {
        //Se já há duas pessoas na sala, inicia o jogo
        if(!deuErro && PhotonNetwork.PlayerList.Length == 2)
        {
            readyObj.SetActive(true);

            //Apenas quem criou a sala pode dar GO no jogo
            if (PhotonNetwork.IsMasterClient)
            {
                goBtn.SetActive(true);
            }

            //goBtn.SetActive(true);

        }

        pingText.text = PhotonNetwork.GetPing().ToString();
    }

    //Callback de erro ao se juntar a sala
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        deuErro = true;
    }

    public void BackToMenu()
    {
        PhotonNetwork.LoadLevel(menuSceneName);
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
        PhotonNetwork.LoadLevel(gameSceneName);
    }
}
