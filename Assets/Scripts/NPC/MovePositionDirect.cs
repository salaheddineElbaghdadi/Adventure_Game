using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour
{
    public event EventHandler OnPositionReached;

    [SerializeField] private float speed = 4f;
    private Vector3 movePosition;
    private float eps = 0.1f;

    public void SetMovePosition(Vector3 movePosition) {
        this.movePosition = movePosition;
    }

    private void Update() {
        if ((movePosition - transform.position).magnitude >= eps) {
            Vector3 moveDir = (movePosition - transform.position).normalized;
            GetComponent<IMoveVelocity>().SetVelocity(moveDir * speed);
            Debug.Log(moveDir.magnitude);
        }
        else {
            GetComponent<IMoveVelocity>().SetVelocity(Vector3.zero);
            if (OnPositionReached != null)
                OnPositionReached(this, EventArgs.Empty);
        }
    }
}
