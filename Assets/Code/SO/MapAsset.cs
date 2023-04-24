using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Map/New Map")]
public class MapAsset : ScriptableObject
{
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _spawnPoints;

    public GameObject Map => _map;
    public GameObject SpawnPoints => _spawnPoints;
}
