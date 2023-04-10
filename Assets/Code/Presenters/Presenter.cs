using UnityEngine;

public class Presenter : MonoBehaviour, IPresenter
{
    private Unit _model;
    private readonly float _dieDelay = 7f;

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
        Destroy(gameObject, _dieDelay);
    }
}

