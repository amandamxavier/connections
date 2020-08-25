using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Rigidbody2D rigidbodyRef;
    public float maxMovementSpeed = 1f;
    public float inputOverlapRadius = 1f;
    public float targetDistThreshold = 0.1f;
    public float speedThreshold = 0.05f;
    public string uiRegionTypeToIgnoreInput;

    private float xTargetPos = 0f;
    private bool hasTarget = false;
    private bool isLookingRight = true;
    private SpriteRenderer spriteRendererRef;
    private SpriteRenderer SpriteRendererRef {
        get {
            if (spriteRendererRef == null) {
                spriteRendererRef = GetComponentInChildren<SpriteRenderer>();
            }
            return spriteRendererRef;
        }
    }

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

    private void Start() {
        xTargetPos = transform.position.x;
    }
    private void Update() {
        if (!NetworkingPause.IsPaused) {
            if (Input.GetMouseButtonDown(0) && !UIRegion.ContainsMousePos(uiRegionTypeToIgnoreInput)) {
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
            SpriteRendererRef.flipX = isLookingRight;
        }
    }
    private void FixedUpdate() {
        if (!NetworkingPause.IsPaused && hasTarget) {
            float distToTarget = xTargetPos - transform.position.x;
            if (Mathf.Abs(distToTarget) >= targetDistThreshold) { //only move if is far from target
                rigidbodyRef.MovePosition(transform.position + Vector3.right * maxMovementSpeed * Time.fixedDeltaTime * Mathf.Sign(distToTarget));
            }
            else {
                hasTarget = false;
                //Trigger interaction
            }
        }
    }

}
