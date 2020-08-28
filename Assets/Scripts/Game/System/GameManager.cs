using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Text pingText;

    public GameObject playerPrefab;
    public Transform playerContainer;

    public Transform masterSpawnPoint;
    public Transform otherSpawnPoint;

    void Start()
    {
        SpawnPlayer();
    }

    void Update()
    {
        pingText.text = PhotonNetwork.GetPing().ToString();
    }

    void SpawnPlayer()
    {
        //Instancia o objeto do player através do PhotonNetwork, para todo mundo
        GameObject myPlayer;
        if (PhotonNetwork.IsMasterClient) {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, masterSpawnPoint.position, masterSpawnPoint.rotation);
        }
        else {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, otherSpawnPoint.position, otherSpawnPoint.rotation);
        }
        myPlayer.transform.parent = playerContainer;
    }

    public void CallDisconnectGame()
    {
        photonView.RPC("DisconnectGame", RpcTarget.All);
    }
   
    [PunRPC]
    public void DisconnectGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }

}
