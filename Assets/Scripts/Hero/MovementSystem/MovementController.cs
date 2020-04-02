using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : Activable
{

    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float dashDistance = 30f;

    private Vector2 movementDirection;
    private Movement movement;


    void Start() {
        movement = gameObject.GetComponent<Movement>();
    }

    void Update()
    {
        // input to change later
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        if (IsActive) {
            movement.Move(movementDirection, maxSpeed);
            // dash
            if (Input.GetKeyDown(KeyCode.Space)) {
                movement.Dash(movementDirection, dashDistance);
            }
        }
    }
}
