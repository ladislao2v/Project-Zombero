using System;
using UnityEngine;

public class Hands : Gun
{
    private Zombie _owner;
    private float _lastKick = 0;
    private float _kickRate = 0;
    private float _attackDistance = 0.5f;

    public float Range { get; private set; }

    public event Action Kicked;

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

        _lastKick += Time.deltaTime;

        if (Physics.Raycast(unit.position, DirectionToTarget, out RaycastHit target, _attackDistance))
        {
            if (_lastKick > _kickRate)
            {
                //target.collider.GetComponent<UnitPresenter>().Model.Health.TakeDamage(100);

                Kicked?.Invoke();

                _kickRate = 0.3f;
                _lastKick = 0f;
            }
        }
    }
}
