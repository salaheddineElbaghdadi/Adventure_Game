using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    public bool IsActive {get; set;}  = true;

    public void Activate() {
        IsActive = true;
    }

    public void Disactivate() {
        IsActive = false;
    }
}
