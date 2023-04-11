using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private BulletPresenter _bulletTemplate;

    [SerializeField] private List<ZombiePresenter> _zombieTamplates;

    public void CreateBullet(Bullet bullet, Vector3 position)
    {
        BulletPresenter bulletPresenter = Instantiate(_bulletTemplate, position, Quaternion.identity);
        bulletPresenter.Init(bullet);
    }

    public void CreateZombie(Vector3 position)
    {
        var enemyTemplate = _zombieTamplates[Random.Range(0, _zombieTamplates.Count)];
        var enemy = Instantiate(enemyTemplate, position, Quaternion.identity);
        var zombie = new Zombie(400);

        enemy.Init(zombie);
        enemy.Init((Unit)zombie);
        enemy.GetComponentInChildren<GunPresenter>().Init(new Hands(enemy.Zombie, 10));

        enemy.transform.LookAt(-transform.forward);
    }
}