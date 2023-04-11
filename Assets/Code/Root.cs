using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private List<Transform> _spawnTransforms;


    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private GunPresenter _gunPlayerPresenter;
    [SerializeField] private CamPresenter _camPresenter;
    [SerializeField] private Joystick _joystick;

    private Player _player;
    private Gun _gun;
    private Cam _cam;
    private UserInputHandler _inputHandler;

    public Player Player => _player;

    private void Awake()
    {
        _player = new Player(750);

        _cam = new Cam();
        _gun = new Gun(_player, 8);
        
        _playerPresenter.Init((Unit)_player);
        _playerPresenter.Init(_player);
        _gunPlayerPresenter.Init(_gun);

        _camPresenter.Init(_cam, _playerPresenter);

        _inputHandler = new UserInputHandler(_player, _joystick);
        _inputHandler.OnEnable();
        _gun.Shot += OnShot;

        foreach (var transform in _spawnTransforms)
            _factory.CreateZombie(transform.position);
    }

    private void OnShot(Bullet bullet)
    {
        _factory.CreateBullet(bullet, _gunPlayerPresenter.transform.position);
    }

    private void OnDisable()
    {
        _inputHandler.OnDisable();
        _gun.Shot -= OnShot;
    }

    private void Update()
    {
        _inputHandler.Update();
    }
}
