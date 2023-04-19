using System;
using UnityEngine;

public abstract class Presenter : MonoBehaviour, IPresenter
{
    private IModel _model;

    public IModel Model => _model;
    public event Action<IModel> Inited;

    public void Init(IModel model)
    {
        _model = model;

        Inited?.Invoke(model);
    }


    private void OnEnable()
    {
        Inited += OnInit;
    }

    private void OnDisable()
    {
        Inited -= OnInit;
    }

    protected abstract void OnInit(IModel model);
}

