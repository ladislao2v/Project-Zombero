using UnityEngine;

public class UnitPresenter : MonoBehaviour, IPresenter
{
    private Unit _model;
    protected readonly float DieDelay = 4.5f;

    public Unit Model => _model;

    public void Init(Unit unit)
    {
        _model = unit;

        _model.Moved += OnMoved;
        _model.Rotated += OnRotated;
        _model.Health.Died += OnDied;
    }

    private void OnDisable()
    {
        _model.Moved -= OnMoved;
        _model.Rotated -= OnRotated;
        _model.Health.Died -= OnDied;
    }

    public virtual void OnMoved() { }

    public virtual void OnRotated() { }

    public virtual void OnDied()
    {
        Invoke(nameof(Die), DieDelay);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}

