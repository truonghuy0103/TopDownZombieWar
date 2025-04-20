using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : SingletonMono<MissionControl>
{
    public List<Transform> listCreateZombiePositions = new List<Transform>();
    public int maxZombieCount = 10;
    private int _currentZombieCount = 0;
    
    private int _idZombie = 1;
    private float _timeCountdownCreate = 3;

    public void OnSetupMission()
    {
        //Load Config Mission
        _currentZombieCount = 0;
        
        StartCoroutine(DelayCreateZombie());
    }

    private void CreateZombie(int idZombie)
    {
        int randomPosition = Random.Range(0, listCreateZombiePositions.Count);
        Transform positionToSpawn = listCreateZombiePositions[randomPosition];
        
        ConfigEnemyData configEnemyData = ConfigManager.Instance.configEnemy.GetEnemyDataById(idZombie.ToString());

        GameObject goEnemy = Instantiate(configEnemyData.prefab);
        ZombieNormalSystem zombieNormalSystem = goEnemy.GetComponent<ZombieNormalSystem>();
        zombieNormalSystem.transform.position = positionToSpawn.position;
        zombieNormalSystem.OnSetupZombie(configEnemyData);
        zombieNormalSystem.OnZombieDead += OnZombieDeadCallback;
    }

    private void OnZombieDeadCallback(ZombieSystem zombieSystem)
    {
        _currentZombieCount++;
        if (_currentZombieCount < maxZombieCount)
        {
            //Create zombie
            StartCoroutine(DelayCreateZombie());
        }
        else
        {
            Debug.Log("You win");
        }
    }

    IEnumerator DelayCreateZombie()
    {
        yield return new WaitForSeconds(_timeCountdownCreate);
        CreateZombie(_idZombie);
    }
}
