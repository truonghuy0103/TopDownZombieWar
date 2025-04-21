using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigMission", menuName = "Config/Config Mission")]
public class ConfigMission : ScriptableObject
{
    public List<ConfigMissionData> missions = new List<ConfigMissionData>();
    
    public ConfigMissionData GetMissionDataById(string id)
    {
        return missions.Find(e => e.id == id);
    }
}

[Serializable]
public class ConfigMissionData
{
    public string id;
    public float duration;
    public List<ConfigPhaseData> phases = new List<ConfigPhaseData>();
}

[Serializable]
public class ConfigPhaseData
{
    public string id;
    public float duration;
    public List<ConfigZombieSpawnData> zombieSpawns = new List<ConfigZombieSpawnData>();
}

[Serializable]
public class ConfigZombieSpawnData
{
    public ZombieType type;
    public float spawnInterval;
}
