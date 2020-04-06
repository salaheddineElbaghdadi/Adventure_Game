using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
{
    private Vector3 velocityVector;
    
    public void SetVelocity(Vector3 velocityVector) {
        this.velocityVector = velocityVector;
    }

    public void MoveTowards(Vector3 position, float velocity) {
        transform.position = Vector3.MoveTowards(transform.position, position, velocity);
    }

    void Update() {
        transform.position += velocityVector * Time.deltaTime;
    }
}
