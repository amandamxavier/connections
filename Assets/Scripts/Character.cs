using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Rigidbody2D rigidbodyRef;
    public float maxMovementSpeed = 1f;
    public float inputOverlapRadius = 1f;
    public float targetDistThreshold = 0.1f;
    public float speedThreshold = 0.05f;

    private float xTargetPos = 0f;
    private bool hasTarget = false;
    private bool isLookingRight = true;

    public bool IsWalking {
        get {
            return Mathf.Abs(rigidbodyRef.velocity.x) >= speedThreshold;
        }
    }
    public bool ShouldStartLookingRight {
        get {
            return rigidbodyRef.velocity.x >= speedThreshold;
        }
    }
    public bool ShouldStartLookingLeft {
        get {
            return rigidbodyRef.velocity.x <= -speedThreshold;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            xTargetPos = mouseWorldPos.x; //walk to that position
            hasTarget = true;
            Collider2D clickedCollider = Physics2D.OverlapCircle(mouseWorldPos, inputOverlapRadius); //clicked something?
            if (clickedCollider != null) {
                //Try to find interaction component in clickedCollider
            }
        }

        if (isLookingRight && ShouldStartLookingLeft) {
            isLookingRight = false;
        }
        else if (!isLookingRight && ShouldStartLookingRight) {
            isLookingRight = true;
        }
        //Set animator bool (IsWalking)
        //set sprite flipped (isLookingRight)
    }
    private void FixedUpdate() {
        float distToTarget = xTargetPos - transform.position.x;
        if (Mathf.Abs(distToTarget) >= targetDistThreshold) { //only move if is far from target
            rigidbodyRef.MovePosition(transform.position + Vector3.right * maxMovementSpeed * Time.fixedDeltaTime * Mathf.Sign(distToTarget));
        }
        else if (hasTarget) {
            hasTarget = false;
            //Trigger interaction
        }
    }

}
