using UnityEngine;

public class BulletPresenter : MonoBehaviour
{
    private Bullet _bulletModel;
    private Rigidbody _rigidBody;
    public void Init(Bullet bullet)
    {
        _bulletModel = bullet;
        _bulletModel.Destroyed += OnDestroyed;
    }

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _bulletModel.Destroyed -= OnDestroyed;
    }

    private void FixedUpdate()
    {
        _rigidBody.AddForce(_bulletModel.MoveVector(Time.fixedDeltaTime).normalized * _bulletModel.Speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Presenter presenter))
            presenter.Model.Health.TakeDamage(_bulletModel.Damage);

        Destroy(gameObject);
    }
}