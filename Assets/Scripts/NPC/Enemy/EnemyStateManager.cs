using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Initial, HeroInRange, HeroOutOfRange
}

[RequireComponent(typeof(EnemyAI))]
public class EnemyStateManager : MonoBehaviour
{
    #region fields

    public event EventHandler HeroInRange;
    public event EventHandler HeroOutOfRange;

    [SerializeField] private Transform hero;
    [SerializeField] private EnemyState _state;
    //private EnemyAIData AIData;
    private EnemyAI enemyAI;

    #endregion

    #region properties

    public EnemyState State {
        get {
            return _state;
        }
    }

    #endregion

    #region methods

    void Start() {
        //AIData = gameObject.GetComponent<EnemyAI>().AIData;
        enemyAI = gameObject.GetComponent<EnemyAI>();
        _state = EnemyState.Initial;
    }

    void Update() {
        // Update State
        CheckForHeroInRange();
        CheckForHeroOutOfRange();
    }


    private void CheckForHeroInRange() {
        if ((hero.position - transform.position).magnitude <= enemyAI.AIData.heroRangeDetection && _state != EnemyState.HeroInRange) {
            _state = EnemyState.HeroInRange;
            HeroInRange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void CheckForHeroOutOfRange() {
        if((hero.position - transform.position).magnitude > enemyAI.AIData.heroRangeDetection && _state == EnemyState.HeroInRange) {
            _state = EnemyState.HeroOutOfRange;
            HeroOutOfRange?.Invoke(this, EventArgs.Empty);
        }
    }

    #endregion

}
