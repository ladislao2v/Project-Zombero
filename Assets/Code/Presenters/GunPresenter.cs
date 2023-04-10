using UnityEngine;

public class GunPresenter : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    private Gun _gunModel;
    //private Animator _animator;

    private const string _shoot = "Shoot";

    public virtual void Init(Gun gun)
    {
        _gunModel = gun;
        _gunModel.GetTargetsMask(_targetMask);

        _gunModel.Shot += OnShot;
    }

    private void OnDisable()
    {
        _gunModel.Shot -= OnShot;
    }

    private void Update()
    {
        _gunModel.FindTarget(transform);

        _gunModel.Recharge(Time.deltaTime);
    }

    protected void OnShot(Bullet bullet)
    {
        //_animator.SetTrigger(_shoot);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_gunModel?.NearestTarget)
            Gizmos.DrawLine(transform.position, _gunModel.NearestTarget.position);
    }
}