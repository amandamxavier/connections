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
    public float minReachHeight = -0.2f;
    public float maxReachHeight = 0.3f;

    private float xTargetPos = 0f;
    private bool hasTarget = false;
    private bool isLookingRight = true;
    private Interactable targetInteractable;
    private Obstacle targetObstacle;

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
        Gizmos.color = Color.yellow;
        Vector3 reachCenter = new Vector3(transform.position.x, transform.position.y + (maxReachHeight + minReachHeight) / 2f, transform.position.z);
        Gizmos.DrawWireCube(reachCenter, new Vector3(10f, maxReachHeight - minReachHeight, 1f));
    }

    private void Update() {

        if (!NetworkingPause.IsPaused) {

            //Pega posição do mouse para andar
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //clicked something?
            Collider2D clickedCollider = Physics2D.OverlapCircle(mouseWorldPos, inputOverlapRadius, overlapLayerMask);
            Interactable interactableObj = null;
            Obstacle obstacleObj = null;

            if (clickedCollider != null) {
                float deltaY = clickedCollider.transform.position.y - transform.position.y;
                //only objects within reach
                if (deltaY >= minReachHeight && deltaY <= maxReachHeight) {
                    //Objeto interativo
                    if (clickedCollider.gameObject.CompareTag("Interactables")) {
                        interactableObj = clickedCollider.GetComponent<Interactable>();
                    }
                    //Obstáculos
                    else if (clickedCollider.gameObject.CompareTag("Obstacles")) {
                        obstacleObj = clickedCollider.GetComponent<Obstacle>();
                    }

                }
            }
            Interactable.hovered = interactableObj;

            //interact with the objects
            if (Input.GetMouseButtonDown(0) && !UIRegion.ContainsMousePos(uiRegionTypeToIgnoreInput)) {
                //walk to that position
                xTargetPos = mouseWorldPos.x;
                hasTarget = true;
                targetInteractable = interactableObj;
                targetObstacle = obstacleObj;
            }

            if (isLookingRight && ShouldStartLookingLeft) {
                isLookingRight = false;
            }
            else if (!isLookingRight && ShouldStartLookingRight) {
                isLookingRight = true;
            }

            if(gameObject != null)
            {
                AnimatorRef?.SetBool(walkAnimParameterName, IsWalking);
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
                //Se o objeto está ao alance e é utilizável
                if (targetInteractable != null && targetInteractable.isUsable) {
                    //Coloca o objeto no inventário e o desativa
                    inventory.SetItem(targetInteractable.key, targetInteractable.value);
                    targetInteractable.CollectObj();
                }
                //Se tem o item necessário para destruir aquele obstáculo
                if (targetObstacle != null && inventory.GetItem(targetObstacle.id.ToString())) {
                    targetObstacle.DestroyObstacle();
                }
                targetInteractable = null;
                targetObstacle = null;
            }
        }
    }

}
