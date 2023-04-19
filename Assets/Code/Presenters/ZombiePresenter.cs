using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombiePresenter : UnitPresenter
{
    private Zombie _zombieModel;
    private NavMeshAgent _agent;
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private float _lastKick = 0;
    private float _kickRate = 0;

    private const string _isRun = "isRun";
    private const string _shoot = "Shoot";
    private const string _death = "Death";

    public Zombie Zombie => _zombieModel;   

    public void Init(Zombie zombie)
    {
        _zombieModel = zombie;
    }

    public override void OnMoved()
    {
        if (_agent.enabled)
        {
            _agent.SetDestination(_zombieModel.Move(transform.position, Time.fixedDeltaTime));
        }
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if(_agent.destination != transform.position)
            _animator.SetBool(_isRun, true);
        else
            _animator.SetBool(_isRun, false);
    }

    private void FixedUpdate()
    {
        _zombieModel.MoveUnit();
        _zombieModel.RotateUnit();
    }

    public override void OnDied()
    {
        _capsuleCollider.enabled = false;
        _agent.enabled = false;

        _animator.SetBool(_isRun, false);
        _animator.SetTrigger(_death);

        base.OnDied();
    }

    private void OnCollisionStay(Collision collision)
    {
        _lastKick += Time.deltaTime;

        if (_lastKick > _kickRate)
        {
            if (collision.gameObject.TryGetComponent(out PlayerPresenter presenter))
            {
                Debug.Log(collision.gameObject.name);
                _animator.SetTrigger(_shoot);

                presenter.Model.Health.TakeDamage(_zombieModel.Damage);
            }

            _kickRate = 0.3f;
            _lastKick = 0f;
        }

    }
}
