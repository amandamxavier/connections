using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererView : MonoBehaviour, IPunObservable
{

    public SpriteRenderer spriteRenderer;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (spriteRenderer != null) {
            if (stream.IsWriting) {
                stream.SendNext(spriteRenderer.flipX);
            }
            else {
                spriteRenderer.flipX = (bool)stream.ReceiveNext();
            }
        }
    }

}
