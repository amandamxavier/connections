using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Responsável pelas operações de conexão com o servidor do Photon
public class ConnectionManager : MonoBehaviour
{
    public Text connectionInfo, pingInfo;
    public Button playBtn;

    void Awake()
    {
        //Onde ocorre a conexão com o master server
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        //Informações de conexão e ping
        connectionInfo.text = PhotonNetwork.NetworkClientState.ToString();
        pingInfo.text = PhotonNetwork.GetPing().ToString() + "ms";

        //Ir para o lobby só se tiver conectado
        playBtn.interactable = PhotonNetwork.IsConnectedAndReady;
    }
}
