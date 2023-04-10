using UnityEngine;

public class CamPresenter : MonoBehaviour
{
    private Cam _camModel;
    private PlayerPresenter _playerPresenter;


    public void Init(Cam camModel, PlayerPresenter playerPresenter)
    {
        _camModel = camModel;
        _playerPresenter = playerPresenter;
    }

    private void Start()
    {
        _camModel.GetStartPosition(transform.position);
    }

    private void Update()
    {
        transform.position = _camModel.Follow(transform, _playerPresenter.transform);
    }
}