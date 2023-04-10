using System;
using UnityEngine;

public class Gun : IGun
{
    private Unit _owner;
    private int _bulletsPerShot;
    private float _fireRate = 0f;
    private float _lastFire = 0f;
    private float _rechargeDelay = 1.5f;
    private float _lastRecharge = 0f;
    private LayerMask _mask;
    private Transform _nearestTarget;
    

    public Transform NearestTarget => _nearestTarget;

    public Vector3 DirectionToTarget { get; protected set; }
    public int Bullets { get; private set; }

    public event Action<Bullet> Shot;

    public Gun(Unit owner,int bullets,int bulletsPerShot = 1)
    {
        _owner = owner;
        Bullets = bullets;
        _bulletsPerShot = bulletsPerShot;
    }

    public void GetTargetsMask(LayerMask mask)
    {
        _mask = mask;
    }

    public void FindTarget(Transform gun)
    {
        if (!_owner.IsStay())
            return;

        var targetsInRange = Physics.OverlapSphere(gun.position, float.MaxValue, _mask);

        if (targetsInRange.Length > 0)
        {
            foreach (var target in targetsInRange)
            {
                var distance = Vector3.Distance(gun.position, target.transform.position);
                var distanceToNeareastTarget = float.MaxValue;

                if (distance < distanceToNeareastTarget)
                {
                    _nearestTarget = target.transform;
                }
            }
        }

        if(_nearestTarget)
        {
            DirectionToTarget = _nearestTarget.position - gun.position;

            if (Physics.Raycast(gun.position, DirectionToTarget, out RaycastHit hit, float.MaxValue))
            {
                if (hit.collider.gameObject.TryGetComponent(out Presenter presenter))
                {
                    if (CanShoot())
                    {
                        Attack(gun.parent);
                    }
                }
            }
              
        }
    }

    public void Recharge(float time)
    {
        _lastRecharge += time;

        if (_lastRecharge > _rechargeDelay && Bullets < 3)
        {
            Bullets++;

            _lastRecharge = 0;
        }
    }

    protected virtual bool CanShoot() => Bullets >= _bulletsPerShot;

    protected virtual void Attack(Transform unit)
    {
        _lastFire += Time.deltaTime;

        if (_lastFire > _fireRate)
        {
            unit.LookAt(NearestTarget);

            Bullet bullet = GetBullet();
            bullet.GetDirection(DirectionToTarget.normalized);

            Shot.Invoke(bullet);

            _fireRate = 0.3f;
            _lastFire = 0f;
        }
    }

    protected virtual Bullet GetBullet()
    {
        Bullets -= _bulletsPerShot;

        return new Bullet(100, 5f, 110f);
    }

}

public interface IGun
{
    void FindTarget(Transform gun);
}