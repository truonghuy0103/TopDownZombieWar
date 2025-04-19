using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigGun", menuName = "Config/Config Gun")]
public class ConfigGun : ScriptableObject
{
    public List<GunData> Guns = new List<GunData>();

    public GunData GetGunById(string id)
    {
        return Guns.Find(g => g.id == id);
    }
}

[Serializable]
public class GunData
{
    public string id;
    public string name;
    public float rangeOfFire;
    public int damage;
    public int clipSize;
    public int amountAmo;
    public float timeReload;
}
