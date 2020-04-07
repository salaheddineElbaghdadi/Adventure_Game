using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType {
    Stationery, Random, FollowHero, BackToOrigin
}


[RequireComponent(typeof(EnemyAI))]
public class MovementSystem : MonoBehaviour
{
    #region fields

    private EnemyAI AI;
    private EnemyStateManager stateManager;

    #endregion
    

    #region methods

    void Start() {
        AI = gameObject.GetComponent<EnemyAI>();
        stateManager = gameObject.GetComponent<EnemyStateManager>();
        stateManager.HeroInRange += OnHeroInRange;
        stateManager.HeroOutOfRange += OnHeroOutOfRange;
    }


    private void OnHeroInRange(object sender, EventArgs e) {
        Debug.Log("Hero in ranger ----> Attaaaaack !!!");
    }

    private void OnHeroOutOfRange(object sender, EventArgs e) {
        Debug.Log("OHh .... he is not here !");
    }

    #endregion
}
