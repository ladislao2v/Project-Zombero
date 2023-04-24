using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private List<MapAsset> _mapAssets;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private Transform _map;
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private GunPresenter _gunPlayerPresenter;
    [SerializeField] private CamPresenter _camPresenter;
    [SerializeField] private Joystick _joystick;

    private Player _player;
    private Gun _gun;
    private Cam _cam;
    private UserInputHandler _inputHandler;
    private List<Transform> _spawnTransforms;

    public Player Player => _player;

    private void Awake()
    {
        _surface?.BuildNavMesh();
        Debug.ClearDeveloperConsole();

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

        BuildLevel();
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

    private void BuildLevel()
    {
        GenerateMap();

        foreach (var transform in _spawnTransforms)
            _factory.CreateZombie(transform.position);
    }

    private void GenerateMap()
    {
        var mapAsset = _mapAssets[Random.Range(0, _mapAssets.Count)];

        var map = Instantiate(mapAsset.Map, Vector3.zero , Quaternion.identity, _map);
        var points = Instantiate(mapAsset.SpawnPoints, Vector3.zero, Quaternion.identity, _map);

        _spawnTransforms = points.GetComponentsInChildren<Transform>().ToList();

        foreach (var transform in _spawnTransforms)
        {
            if (transform.position == points.transform.position)
            {
                _spawnTransforms.Remove(transform);

                break;
            }
        }
    }
}
