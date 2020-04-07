using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Enemy AI Data", menuName="Game/Enemy AI")]
public class EnemyAIData : ScriptableObject
{
    public float heroRangeDetection;
    public float maxSpeed;

    public MovementType initialStateMoveType;
    public MovementType heroInRangeMoveType;
    public MovementType heroOutOfRnange;
}