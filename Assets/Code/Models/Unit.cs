using System;
using UnityEngine;

public abstract class Unit
{
    public IHealth Health { get; private set; }

    public bool IsStay() => Direction == Vector3.zero;
    public Vector3 Direction { get; protected set; }

    public event Action Moved;
    public event Action Rotated;

    public Unit(int healthValue)
    {
        Health = new Health(healthValue);
    }

    public void RotateUnit()
    {
        Rotated?.Invoke();
    }

    public void MoveUnit()
    {
        Moved?.Invoke();
    }

    public virtual Quaternion Rotate(Transform presenter)
    {
        return presenter.rotation;
    }

    public virtual Vector3 Move(Vector3 position, float deltaTime) 
    {
        return Vector3.zero;
    }
}
