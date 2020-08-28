using UnityEngine;

public class ParalaxMovement : MonoBehaviour {

    public Transform referenceTransform;
    public float movementMultiplier;

    private Vector3 ownOrigin;
    private Vector3 refOrigin;

    private void Start() {
        ownOrigin = transform.position;
        refOrigin = referenceTransform.position;
    }
    private void Update() {
        transform.position = ownOrigin + (referenceTransform.position - refOrigin) * movementMultiplier;
    }

}