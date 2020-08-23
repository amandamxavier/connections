using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public Transform spawnPoint;

    void Start()
    {
        SpawnPlayer();
    }

    void Update()
    {
        
    }

    void SpawnPlayer()
    {
        GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}
