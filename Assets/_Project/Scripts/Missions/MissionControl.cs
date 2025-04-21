using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionControl : SingletonMono<MissionControl>
{
    public List<Transform> listCreateZombiePositions = new List<Transform>();

    [SerializeField] private ZombieNormalSystem _zomNromalPrefab;
    [SerializeField] private int _maxZomNormal = 10;
    private Queue<ZombieNormalSystem> _zomNormalPool = new Queue<ZombieNormalSystem>();
    private int _countZomNormal = 0;

    private float _timeCountdownCreate = 3f;

    public void OnSetupMission()
    {
        //Load Config Mission
        
        SetUpZombieNormalPool();

        StartCoroutine(SpawnZombieNormal());
    }

    IEnumerator SpawnZombieNormal()
    {
        while (_countZomNormal < _maxZomNormal)
        {
            yield return new WaitForSeconds(_timeCountdownCreate);
            CreateZombieNormal();
        }
    }

    private void SetUpZombieNormalPool()
    {
        ConfigEnemyData configEnemyData = ConfigManager.Instance.configEnemy.GetEnemyDataById(1.ToString());
        for (int i = 0; i < _maxZomNormal; i++)
        {
            ZombieNormalSystem zombieNormal = Instantiate(_zomNromalPrefab);
            zombieNormal.gameObject.SetActive(false);
            zombieNormal.OnSetupZombie(configEnemyData);
            zombieNormal.OnZombieDead += OnZombieDeadCallback;
            _zomNormalPool.Enqueue(zombieNormal);
        }
    }

    private ZombieNormalSystem GetZombieNormalFromPool()
    {
        ZombieNormalSystem zombie = _zomNormalPool.Dequeue();
        zombie.gameObject.SetActive(true);
        _countZomNormal++;
        return zombie;
    }

    private void ReturnZombieNormalToPool(ZombieNormalSystem zombie)
    {
        zombie.ResetZombie();
        zombie.gameObject.SetActive(false);
        _zomNormalPool.Enqueue(zombie);
        _countZomNormal--;
    }

    private void OnZombieDeadCallback(ZombieSystem zombieNormal)
    {
        ReturnZombieNormalToPool(zombieNormal as ZombieNormalSystem);
        if (_countZomNormal < _maxZomNormal)
        {
            StartCoroutine(DelayCreateZombie());
        }
    }

    private void CreateZombieNormal()
    {
        int randomPosition = Random.Range(0, listCreateZombiePositions.Count);
        Transform positionToSpawn = listCreateZombiePositions[randomPosition];

        ZombieNormalSystem zombieNormal = GetZombieNormalFromPool();
        zombieNormal.transform.position = positionToSpawn.position;
    }
    
    IEnumerator DelayCreateZombie()
    {
        yield return new WaitForSeconds(_timeCountdownCreate);
        CreateZombieNormal();
    }
    
    
}
