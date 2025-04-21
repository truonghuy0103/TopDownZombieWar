using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionControl : MonoBehaviour
{
    [SerializeField] private List<Transform> listCreateZombiePositions = new List<Transform>();
    [SerializeField] private Transform _holderZombie;

    [SerializeField] private ZombieNormalSystem _zomNromalPrefab;
    [SerializeField] private int _maxZomNormal = 10;
    private Queue<ZombieNormalSystem> _zomNormalPool = new Queue<ZombieNormalSystem>();
    private int _countZomNormal = 0;

    private ConfigMissionData _configMissionData;
    private Dictionary<ZombieType, float> _dictIntervals = new Dictionary<ZombieType, float>();

    public void OnSetupMission(int mission)
    {
        //Load Config Mission
        _configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(mission.ToString());

        SetUpZombieNormalPool();

        StopCoroutine(RunMission());
        StartCoroutine(RunMission());
    }

    private IEnumerator RunMission()
    {
        foreach (var phase in _configMissionData.phases)
        {
            StopCoroutine(SpawnZombieNormal());
            foreach (var zombie in phase.zombieSpawns)
            {
                SetupSpawnIntervalForZombie(zombie);
            }

            StartCoroutine(SpawnZombieNormal());
            yield return new WaitForSeconds(phase.duration);
        }
    }

    private void SetupSpawnIntervalForZombie(ConfigZombieSpawnData zombieSpawnData)
    {
        if (!_dictIntervals.ContainsKey(zombieSpawnData.type))
        {
            _dictIntervals.Add(zombieSpawnData.type, zombieSpawnData.spawnInterval);
        }

        _dictIntervals[zombieSpawnData.type] = zombieSpawnData.spawnInterval;
    }
    
    private void OnZombieDeadCallback(ZombieSystem zombieNormal)
    {
        ReturnZombieNormalToPool(zombieNormal as ZombieNormalSystem);
        if (_countZomNormal < _maxZomNormal)
        {
            StartCoroutine(DelayCreateZombieNormal());
        }
    }
    
    #region Zombie Normal

    private IEnumerator SpawnZombieNormal()
    {
        while (_countZomNormal < _maxZomNormal)
        {
            yield return new WaitForSeconds(_dictIntervals[ZombieType.Normal]);
            CreateZombieNormal();
        }
    }

    private void SetUpZombieNormalPool()
    {
        if (_zomNormalPool.Count > 0)
            return;
        
        ConfigEnemyData configEnemyData = ConfigManager.Instance.configEnemy.GetEnemyDataById(1.ToString());
        for (int i = 0; i < _maxZomNormal; i++)
        {
            ZombieNormalSystem zombieNormal = Instantiate(_zomNromalPrefab);
            zombieNormal.transform.SetParent(_holderZombie);
            zombieNormal.gameObject.SetActive(false);
            zombieNormal.OnSetupZombie(configEnemyData);
            zombieNormal.OnZombieDead += OnZombieDeadCallback;
            _zomNormalPool.Enqueue(zombieNormal);
        }
    }

    private ZombieNormalSystem GetZombieNormalFromPool()
    {
        if (_zomNormalPool.Count > 0)
        {
            ZombieNormalSystem zombie = _zomNormalPool.Dequeue();
            zombie.gameObject.SetActive(true);
            _countZomNormal++;
            return zombie;
        }

        return null;
    }

    private void ReturnZombieNormalToPool(ZombieNormalSystem zombie)
    {
        zombie.ResetZombie();
        zombie.gameObject.SetActive(false);
        _zomNormalPool.Enqueue(zombie);
        _countZomNormal--;
    }

    private void CreateZombieNormal()
    {
        int randomPosition = Random.Range(0, listCreateZombiePositions.Count);
        Transform positionToSpawn = listCreateZombiePositions[randomPosition];

        ZombieNormalSystem zombieNormal = GetZombieNormalFromPool();
        if (zombieNormal != null)
        {
            zombieNormal.transform.position = positionToSpawn.position;
        }
    }

    IEnumerator DelayCreateZombieNormal()
    {
        yield return new WaitForSeconds(_dictIntervals[ZombieType.Normal]);
        CreateZombieNormal();
    }

    #endregion
}
