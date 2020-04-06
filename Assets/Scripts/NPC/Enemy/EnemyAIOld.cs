using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAIOld : MonoBehaviour
{
    
    private Vector3 startingPosition;
    private Vector3 roamingPosition;

    private IMovePosition movePosition;

    void Start() {
        startingPosition = transform.position;
        roamingPosition = GetRoamingPosition();
        Debug.Log("Roaming position: " + roamingPosition);
        movePosition = gameObject.GetComponent<IMovePosition>();
        gameObject.GetComponent<IMovePosition>().OnPositionReached += OnPositionReachedEventHandler;
        movePosition.SetMovePosition(roamingPosition);
    }

    

    private void OnPositionReachedEventHandler(object sender, EventArgs e) {
        roamingPosition = GetRoamingPosition();
        Debug.Log("Roaming position: " + roamingPosition);
        movePosition.SetMovePosition(roamingPosition);
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(5f, 7f);
        //return new Vector3(0, 0);
    }

}
