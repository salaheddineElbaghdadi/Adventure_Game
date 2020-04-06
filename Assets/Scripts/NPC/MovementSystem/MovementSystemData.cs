using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType {
    Stationery, Random, FollowHero, BackToOrigin
}

[CreateAssetMenu(fileName="MovementSystemData", menuName="Game/MovementSystem")]
public class MovementSystemData : ScriptableObject
{
    public MovementType InitialStateMoveType;
    public MovementType HeroInRangeMoveType;
    public MovementType HeroOutOfRanger;   

    public float maxSpeed;
}