using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseSensor : MonoBehaviour {

    public bool invokeOnStart = false;
    public UnityEvent onPaused;
    public UnityEvent onUnpaused;

    private bool value;

    private void Start() {
        value = NetworkingPause.IsPaused;
        if (invokeOnStart) {
            Invoke();
        }
    }
    private void Update() {
        if (value != NetworkingPause.IsPaused) {
            value = NetworkingPause.IsPaused;
            Invoke();
        }
    }

    private void Invoke() {
        if (value) {
            onPaused.Invoke();
        }
        else {
            onUnpaused.Invoke();
        }
    }

}
