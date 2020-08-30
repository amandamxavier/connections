using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    private void OnDestroy() {
        target = null;
    }

    public Vector3 offset;

    public void Update() {
        if (target != null) {
            transform.position = target.position + offset;
        }
    }

}
