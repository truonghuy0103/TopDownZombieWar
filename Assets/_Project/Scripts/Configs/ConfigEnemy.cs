using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigEnemy", menuName = "Config/Config Enemy")]
public class ConfigEnemy : ScriptableObject
{
    public List<EnemyData> Enemies = new List<EnemyData>();

    public EnemyData GetEnemyDataById(string id)
    {
        return Enemies.Find(e => e.id == id);
    }
}

[Serializable]
public class EnemyData
{
    public string id;
    public string name;
    public int hp;
    public int damage;
    public float rangeOfAttack;
    public string namePrefab;
}
