using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionControl : MonoBehaviour
{
    [SerializeField] private List<Transform> listCreateZombiePositions = new List<Transform>();
    [SerializeField] private Transform _holderZombie;

    [Header("Zombie Normal")]
    [SerializeField] private ZombieNormalSystem _zomNromalPrefab;
    [SerializeField] private int _maxZomNormal = 10;
    private Queue<ZombieNormalSystem> _zomNormalPool = new Queue<ZombieNormalSystem>();
    private int _countZomNormal = 0;

    
    [Header("Zombie Boss")]
    [SerializeField] private ZombieBossSystem _zomBossPrefab;
    [SerializeField] private int _maxZomBoss = 10;
    private Queue<ZombieBossSystem> _zomBossPool = new Queue<ZombieBossSystem>();
    private int _countZomBoss = 0;
    
    private ConfigMissionData _configMissionData;
    private Dictionary<ZombieType, float> _dictIntervals = new Dictionary<ZombieType, float>();

    public void OnSetupMission(int mission)
    {
        //Load Config Mission
        _configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(mission.ToString());

        SetUpZombieNormalPool();
        SetUpZombieBossPool();

        StopCoroutine(RunMission());
        StartCoroutine(RunMission());
    }

    private IEnumerator RunMission()
    {
        foreach (var phase in _configMissionData.phases)
        {
            StopCoroutine(SpawnZombieNormal());
            StopCoroutine(SpawnZombieBoss());
            
            foreach (var zombie in phase.zombieSpawns)
            {
                SetupSpawnIntervalForZombie(zombie);
            }

            if (_dictIntervals.ContainsKey(ZombieType.Normal))
            {
                StartCoroutine(SpawnZombieNormal());
            }

            if (_dictIntervals.ContainsKey(ZombieType.Boss))
            {
                StartCoroutine(SpawnZombieBoss());
            }
       
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
            zombieNormal.OnZombieDead += OnZombieNormalDeadCallback;
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
    
    private void OnZombieNormalDeadCallback(ZombieSystem zombieNormal)
    {
        ReturnZombieNormalToPool(zombieNormal as ZombieNormalSystem);
        if (_countZomNormal < _maxZomNormal)
        {
            StartCoroutine(DelayCreateZombieNormal());
        }
    }

    #endregion
    
    #region Zombie Boss

    private IEnumerator SpawnZombieBoss()
    {
        while (_countZomBoss < _maxZomBoss)
        {
            yield return new WaitForSeconds(_dictIntervals[ZombieType.Boss]);
            CreateZombieBoss();
        }
    }

    private void SetUpZombieBossPool()
    {
        if (_zomBossPool.Count > 0)
            return;
        
        ConfigEnemyData configEnemyData = ConfigManager.Instance.configEnemy.GetEnemyDataById(2.ToString());
        for (int i = 0; i < _maxZomBoss; i++)
        {
            ZombieBossSystem zombieBoss = Instantiate(_zomBossPrefab);
            zombieBoss.transform.SetParent(_holderZombie);
            zombieBoss.gameObject.SetActive(false);
            zombieBoss.OnSetupZombie(configEnemyData);
            zombieBoss.OnZombieDead += OnZombieBossDeadCallback;
            _zomBossPool.Enqueue(zombieBoss);
        }
    }

    private ZombieBossSystem GetZombieBossFromPool()
    {
        if (_zomBossPool.Count > 0)
        {
            ZombieBossSystem zombie = _zomBossPool.Dequeue();
            zombie.gameObject.SetActive(true);
            _countZomBoss++;
            return zombie;
        }

        return null;
    }

    private void ReturnZombieBossToPool(ZombieBossSystem zombie)
    {
        zombie.ResetZombie();
        zombie.gameObject.SetActive(false);
        _zomBossPool.Enqueue(zombie);
        _countZomBoss--;
    }

    private void CreateZombieBoss()
    {
        int randomPosition = Random.Range(0, listCreateZombiePositions.Count);
        Transform positionToSpawn = listCreateZombiePositions[randomPosition];

        ZombieBossSystem zombieBoss = GetZombieBossFromPool();
        if (zombieBoss != null)
        {
            zombieBoss.transform.position = positionToSpawn.position;
        }
    }

    IEnumerator DelayCreateZombieBoss()
    {
        yield return new WaitForSeconds(_dictIntervals[ZombieType.Boss]);
        CreateZombieBoss();
    }
    
    private void OnZombieBossDeadCallback(ZombieSystem zombieNormal)
    {
        ReturnZombieBossToPool(zombieNormal as ZombieBossSystem);
        if (_countZomBoss < _maxZomBoss)
        {
            StartCoroutine(DelayCreateZombieBoss());
        }
    }
    

    #endregion
}
