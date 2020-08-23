using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

//Responsável por gerenciar o funcionamento do Lobby
//MonoBehaviorPunCallBacks necessário
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public Sprite[] roomIcons;

    public void InitLobby()
    {
        //Para se juntar a uma sala, precisa estar no Lobby primeiro
        PhotonNetwork.JoinLobby();

        //Se a conexão foi feita, instancia 10 botões que levam a salas diferentes
        if (PhotonNetwork.IsConnected)
        {
            for (int i = 0; i <= 9; i++)
            {
                Room newRoom = Instantiate(roomPrefab).GetComponent<Room>();
                newRoom.transform.parent = gameObject.transform;
                //Diferenciação das salas pelos nomes
                newRoom.roomName = "Room" + i+1;
                newRoom.roomIcon = roomIcons[i];
            }
        }
    }
}
