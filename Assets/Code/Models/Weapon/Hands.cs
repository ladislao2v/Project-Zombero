using System;
using UnityEngine;

public class Hands : Gun
{
    private Zombie _owner;

    public float Range { get; private set; }

    public Hands(Zombie owner, float range, int bullets = 1, int bulletsPerShot = 1) : base(owner, bullets, bulletsPerShot)
    {
        _owner = owner;
        Range = range;
    }

    protected override void Attack(Transform unit)
    {
        if (Vector3.Distance(unit.position, NearestTarget.position) < 2 * Range)
            _owner.SetTarget(NearestTarget);
        else
            _owner.SetTarget(null);
    }
}
