using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public bool isActive;

    public abstract void Activate();
    public abstract void Deactivate();
}
