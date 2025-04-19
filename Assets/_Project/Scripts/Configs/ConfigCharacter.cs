using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigCharacter", menuName = "Config/Config Character")]
public class ConfigCharacter : ScriptableObject
{
    public List<ConfigCharacterData> Characters = new List<ConfigCharacterData>();

    public ConfigCharacterData GetCharacterDataById(string id)
    {
        return Characters.Find(x => x.id == id);
    }
}

[Serializable]
public class ConfigCharacterData
{
    public string id;
    public string name;
    public int hp;
    public string skinId;
    public string weaponId;
    public string namePrefab;
}