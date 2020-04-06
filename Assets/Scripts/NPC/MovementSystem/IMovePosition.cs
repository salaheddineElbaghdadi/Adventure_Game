using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovePosition
{
    event EventHandler OnPositionReached;

    void SetMovePosition(Vector3 movePosition);
}
