using UnityEngine;

public class GunPresenter : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    private Gun _gunModel;

    public void Init(Gun gun)
    {
        _gunModel = gun;
        _gunModel.GetTargetsMask(_targetMask);
    }

    private void Update()
    {
        _gunModel.FindTarget(transform);

        _gunModel.Recharge(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_gunModel?.NearestTarget)
            Gizmos.DrawLine(transform.position, _gunModel.NearestTarget.position);
    }
}