using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

//Responsável pelas operações de conexão com o servidor do Photon
public class ConnectionManager : MonoBehaviour
{
    [Header("General Connection Info")]
    public Text connectionInfo;
    public Text pingInfo;

    [Header("Menus")]
    public GameObject initPanel;
    public GameObject lobbyPanel;

    [Header("Interactables")]
    public Button playBtn;
    public LobbyManager lobbyManager;
    
    void Awake()
    {
        //Onde ocorre a conexão com o master server
        PhotonNetwork.ConnectUsingSettings();

        //Todos da mesma sala estarão sempre na mesma cena
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        //Informações de conexão e ping
        connectionInfo.text = PhotonNetwork.NetworkClientState.ToString();
        pingInfo.text = PhotonNetwork.GetPing().ToString() + "ms";

        //Ir para o lobby só se tiver conectado
        playBtn.interactable = PhotonNetwork.IsConnectedAndReady;
    }

    public void Play()
    {
        initPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        lobbyManager.InitLobby();
    }
}
