using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ConfigEnemy", menuName = "Config/Config Enemy")]
public class ConfigEnemy : ScriptableObject
{
    public List<ConfigEnemyData> Enemies = new List<ConfigEnemyData>();

    public ConfigEnemyData GetEnemyDataById(string id)
    {
        return Enemies.Find(e => e.id == id);
    }
}

[Serializable]
public class ConfigEnemyData
{
    public string id;
    public ZombieType type;
    public int hp;
    public int damage;
    public float speed;
    public float rateOfAttack;
    public GameObject prefab;
}

public enum ZombieType
{
    Normal = 0,
    Boss = 1,
}
