using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastMoveDirection;

    public void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

    // this method needs to be called from Update()
    public void Move(Vector2 direction, float speed) {
        
        if (TryMove(direction, speed)) {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Magnitude", direction.magnitude);
        }
        else {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Magnitude", 0f);
        }
    }

    private bool TryMove(Vector2 direction, float speed) {

        Vector3 baseMoveDirection = new Vector3(direction.x, direction.y).normalized;
        Vector3 moveDirection = baseMoveDirection;

        bool canMove = CanMove(moveDirection, speed * Time.deltaTime);
        if (!canMove) {
            // can not move diagonally
            moveDirection = new Vector3(baseMoveDirection.x, 0f).normalized;
            canMove = moveDirection.x != 0f && CanMove(moveDirection, speed * Time.deltaTime);
            if (!canMove) {
                // can not move horizontally
                moveDirection = new Vector3(0f, baseMoveDirection.y).normalized;
                canMove = moveDirection.y != 0f && CanMove(moveDirection, speed * Time.deltaTime);
            }
        }

        if (canMove) {
            lastMoveDirection = moveDirection;
            transform.position += moveDirection * speed * Time.deltaTime;
            return true;
        }
        else {
            return false;
        }

    }

    public void Dash(Vector2 direction, float distance) {
        TryMove(direction, distance);
    }

    private bool CanMove(Vector3 direction, float distance) {
        return Physics2D.Raycast(transform.position, direction, distance).collider == null;
    }

    
}
