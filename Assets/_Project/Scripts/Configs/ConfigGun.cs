using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ConfigGun", menuName = "Config/Config Gun")]
public class ConfigGun : ScriptableObject
{
    public List<ConfigGunData> Guns = new List<ConfigGunData>();

    public ConfigGunData GetGunById(string id)
    {
        return Guns.Find(g => g.id == id);
    }
}

[Serializable]
public class ConfigGunData
{
    public string id;
    public string name;
    public float rateOfFire;
    public int damage;
    public int clipSize;
    public int amountAmo;
    public float timeReload;
}
