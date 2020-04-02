using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10f;

    void Update() {
        this.transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
    }
}
