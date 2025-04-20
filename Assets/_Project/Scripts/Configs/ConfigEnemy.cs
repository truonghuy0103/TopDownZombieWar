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
    public string name;
    public int hp;
    public int damage;
    public float speed;
    public float rateOfAttack;
    public string namePrefab;
}
