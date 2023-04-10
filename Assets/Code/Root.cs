using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Factory _factory;

    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private GunPresenter _gunPlayerPresenter;
    [SerializeField] private GunPresenter _gunZombiePresenter;
    [SerializeField] private CamPresenter _camPresenter;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ZombiePresenter _zombiePresenter;

    private Zombie _zombie;
    private Hands _hands;
    private Player _player;
    private Gun _gun;
    private Cam _cam;
    private UserInputHandler _inputHandler;

    public Player Player => _player;

    private void Awake()
    {
        _player = new Player(750);
        _zombie = new Zombie(400);

        _cam = new Cam();
        _gun = new Gun(_player, 8);
        _hands = new Hands(_zombie, 10f);
        

        _playerPresenter.Init((Unit)_player);
        _playerPresenter.Init(_player);

        _zombiePresenter.Init((Unit)_zombie);
        _zombiePresenter.Init(_zombie);

        _gunPlayerPresenter.Init(_gun);
        _gunZombiePresenter.Init(_hands);


        _camPresenter.Init(_cam, _playerPresenter);

        _inputHandler = new UserInputHandler(_player, _joystick);
        _inputHandler.OnEnable();
        _gun.Shot += OnShot;
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
