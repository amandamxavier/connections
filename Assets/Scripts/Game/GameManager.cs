using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Text pingText;

    public GameObject playerPrefab;

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
        if (PhotonNetwork.IsMasterClient) {
            GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, masterSpawnPoint.position, masterSpawnPoint.rotation);
        }
        else {
            GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, otherSpawnPoint.position, otherSpawnPoint.rotation);
        }
    }
}
