using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private BulletPresenter _bulletTemplate;

    [SerializeField] private List<UnitPresenter> _enemies;

    public void CreateBullet(Bullet bullet, Vector3 position)
    {
        BulletPresenter bulletPresenter = Instantiate(_bulletTemplate, position, Quaternion.identity);
        bulletPresenter.Init(bullet);
    }

    public void CreateZombie(Vector3 position)
    {
        var enemyTemplate = _enemies[Random.Range(0, _enemies.Count)];
        var enemy = Instantiate(enemyTemplate, position, Quaternion.identity);

        enemy.Init(new Zombie(400));
    }
}