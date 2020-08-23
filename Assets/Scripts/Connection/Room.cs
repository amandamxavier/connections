using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

//Classe básica de uma sala
public class Room : MonoBehaviour
{
    [Header("Room Infos")]
    public string roomName;
    public Sprite roomIcon;

    //RoomOptions vai definir algumas características das salas
    RoomOptions newRoomOptions = new RoomOptions();

    void Start()
    {
        //Máximo de 2 players e sem publicar suas identificações
        newRoomOptions.MaxPlayers = 2;
        newRoomOptions.PublishUserId = false;

        //Troca a arte do botão via código
        GetComponent<Button>().image.overrideSprite = roomIcon;
    }

    void OnClick()
    {
        //Ao ser clicado, chama a função de criar ou se juntar a uma sala
        PhotonNetwork.JoinOrCreateRoom(roomName, newRoomOptions, TypedLobby.Default);
    }
}
