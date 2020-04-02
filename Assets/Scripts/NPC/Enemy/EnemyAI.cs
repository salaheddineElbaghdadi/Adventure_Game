using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour
{
    
    private Vector3 startingPosition;
    private Vector3 roamingPosition;

    private MovePositionDirect movePositionDirect;

    void Start() {
        startingPosition = transform.position;
        roamingPosition = GetRoamingPosition();
        movePositionDirect = gameObject.GetComponent<MovePositionDirect>();
        gameObject.GetComponent<MovePositionDirect>().OnPositionReached += OnPositionReachedEventHandler;
        movePositionDirect.SetMovePosition(roamingPosition);
    }

    

    private void OnPositionReachedEventHandler(object sender, EventArgs e) {
        roamingPosition = GetRoamingPosition();
        movePositionDirect.SetMovePosition(roamingPosition);
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(1f, 4f);
    }

}
