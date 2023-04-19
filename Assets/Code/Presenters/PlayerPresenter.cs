using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerPresenter : UnitPresenter, IPresenter
{
    private Player _playerModel;
    private NavMeshAgent _agent;
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private GunPresenter _gunPresenter;

    private const string _isRun = "isRun";
    private const string _death = "Death";

    public void Init(Player player)
    {
        _playerModel = player;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _gunPresenter = GetComponentInChildren<GunPresenter>();
    }

    public override void OnMoved()
    {
        if(_agent.enabled == true)
            _agent.SetDestination(_playerModel.Move(transform.position, Time.fixedDeltaTime));
    }

    public override void OnRotated()
    {
        transform.rotation = _playerModel.Rotate(transform);
    }

    public override void OnDied()
    {
        _agent.enabled = false;
        _gunPresenter.enabled = false;
        _capsuleCollider.enabled = false;

        _animator.SetBool(_isRun, false);
        _animator.SetTrigger(_death);

        base.OnDied();
    }

    private void Update()
    {
        if (_playerModel.Direction != Vector3.zero)
            _animator.SetBool(_isRun, true);
        else
            _animator.SetBool(_isRun, false);
    }

    private void FixedUpdate()
    {
        _playerModel.MoveUnit();
        _playerModel.RotateUnit();
    }
}
