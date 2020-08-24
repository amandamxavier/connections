using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Text pingText;

    public GameObject playerPrefab;

    public Transform spawnPoint;

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
        GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}
