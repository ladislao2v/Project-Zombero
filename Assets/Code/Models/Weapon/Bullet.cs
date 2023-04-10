using System;
using UnityEngine;

public class Bullet : IBullet
{
    private float _lifeTime;

    private Vector3 _directionToLastTarget;

    public int Damage { get; private set; }
    public float Speed { get; private set; }

    public event Action Destroyed;

    public Bullet(int damage, float lifeTime, float speed)
    {
        Damage = damage;
        _lifeTime = lifeTime;
        Speed = speed;
    }

    public void GetDirection(Vector3 position)
    {
        _directionToLastTarget = position;
    }

    public Vector3 MoveVector(float time)
    {
        if (_lifeTime > 0)
        {
            _lifeTime -= time;

            return _directionToLastTarget;
        }
        else
        {
            Destroyed?.Invoke();
        }

        return Vector3.zero;
    }
}

public interface IBullet
{
    Vector3 MoveVector(float time);
}
