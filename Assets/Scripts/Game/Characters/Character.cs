using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Rigidbody2D rigidbodyRef;
    public float maxMovementSpeed = 1f;
    public float inputOverlapRadius;
    public LayerMask overlapLayerMask;
    public float targetDistThreshold = 0.1f;
    public float speedThreshold = 0.05f;
    public string uiRegionTypeToIgnoreInput;
    public string walkAnimParameterName = "IsWalking";

    private float xTargetPos = 0f;
    private bool hasTarget = false;
    private bool isLookingRight = true;

    private CharacterInventory inventory;

    private SpriteRenderer spriteRendererRef;
    private SpriteRenderer SpriteRendererRef {
        get {
            if (spriteRendererRef == null) {
                spriteRendererRef = GetComponentInChildren<SpriteRenderer>();
            }
            return spriteRendererRef;
        }
    }
    private Animator animatorRef;
    private Animator AnimatorRef {
        get {
            if (animatorRef == null) {
                animatorRef = GetComponentInChildren<Animator>();
            }
            return animatorRef;
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
        inventory = GetComponent<CharacterInventory>();
    }

    //Debug da posição do mouse apenas
    private void OnDrawGizmos()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mouseWorldPos, inputOverlapRadius);
    }

    private void Update() {

        if (!NetworkingPause.IsPaused) {

            if (Input.GetMouseButtonDown(0) && !UIRegion.ContainsMousePos(uiRegionTypeToIgnoreInput))
            {
                //Pega posição do mouse para andar
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //clicked something?
                Collider2D clickedCollider = Physics2D.OverlapCircle(mouseWorldPos, inputOverlapRadius, overlapLayerMask);

                //interact with the objects
                if (clickedCollider != null)
                {
                    //Objeto interativo
                    if (clickedCollider.gameObject.CompareTag("Interactables"))
                    {
                        Interactable interactableObj = clickedCollider.gameObject.GetComponent<Interactable>();

                        //Se o objeto está ao alance e é utilizável
                        if (interactableObj.isUsable)
                        {
                            //Coloca o objeto no inventário e o desativa
                            inventory.SetItem(interactableObj.key, interactableObj.value);
                            interactableObj.CollectObj();
                        }
                    }
                    //Obstáculos
                    else if (clickedCollider.gameObject.CompareTag("Obstacles"))
                    {
                        Obstacle obstacleObj = clickedCollider.gameObject.GetComponent<Obstacle>();

                        //Se tem o item necessário para destruir aquele obstáculo
                        if(inventory.GetItem(obstacleObj.id.ToString()))
                        {
                            obstacleObj.DestroyObstacle();
                        }
                    }   
                }
                else
                {
                    //walk to that position
                    xTargetPos = mouseWorldPos.x;
                    hasTarget = true;
                }
            }

            if (isLookingRight && ShouldStartLookingLeft) {
                isLookingRight = false;
            }
            else if (!isLookingRight && ShouldStartLookingRight) {
                isLookingRight = true;
            }

            if(gameObject != null)
            {
                AnimatorRef.SetBool(walkAnimParameterName, IsWalking);
                SpriteRendererRef.flipX = isLookingRight;
            }
        }
    }

    private void FixedUpdate() {
        if (!NetworkingPause.IsPaused && hasTarget) {
            float distToTarget = xTargetPos - transform.position.x;
            if (Mathf.Abs(distToTarget) >= targetDistThreshold) { //only move if is far from target
                rigidbodyRef.velocity = Vector3.right * maxMovementSpeed * Mathf.Sign(distToTarget);
            }
            else {
                hasTarget = false;
                rigidbodyRef.velocity = Vector3.zero;
                //Trigger interaction
            }
        }
    }

}
