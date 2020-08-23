using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WatingRoomManager : MonoBehaviourPunCallbacks
{
    public Text userMessage, pingText;
    public GameObject backBtn;
    bool deuErro;

    private void Start()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
    }

    void Update()
    {
        //Se já há duas pessoas na sala, inicia o jogo
        if(!deuErro && PhotonNetwork.PlayerList.Length == 2)
        {
            CallLoadLevel();

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

    void CallLoadLevel()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void BackToMenu()
    {
        PhotonNetwork.LoadLevel("Menu");
        PhotonNetwork.LeaveRoom();
    }
}
