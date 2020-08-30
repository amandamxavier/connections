using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingPause : MonoBehaviourPunCallbacks {

    public static List<NetworkingPause> instanceList = new List<NetworkingPause>();
    private void Awake() {
        IsPaused = false;
        instanceList.Add(this);
    }
    private void OnDestroy() {
        instanceList.Remove(this);
    }
    public static NetworkingPause OwnedInstance {
        get {
            return instanceList.Find(i => i.photonView.IsMine);
        }
    }

    public static Player playerWhoPaused;
    public static bool IsPaused { get; private set; } = false;

    public void TogglePause() {
        if (photonView.IsMine) {
            if (!IsPaused) {
                photonView.RPC("PauseGame", RpcTarget.All, photonView.Owner);
            }
            else if (playerWhoPaused == photonView.Owner) {
                photonView.RPC("UnpauseGame", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void PauseGame(Player whoPaused) {
        playerWhoPaused = whoPaused;
        IsPaused = true;
    }
    [PunRPC]
    private void UnpauseGame() {
        IsPaused = false;
    }

}