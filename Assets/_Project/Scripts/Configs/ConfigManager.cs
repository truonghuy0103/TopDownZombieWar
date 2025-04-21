using UnityEngine;
using System;
using System.Collections;

public class ConfigManager : SingletonMono<ConfigManager>
{
    public ConfigGun configGun;
    public ConfigEnemy configEnemy;
    public ConfigCharacter configCharacter;
    public ConfigMission configMission;
    public void InitConfig(Action callback)
    {
        StartCoroutine(LoadConfig(callback));
    }

    IEnumerator LoadConfig(Action callback)
    {
        Debug.Log("Loading config");
        yield return new WaitForSeconds(0.1f);
        
        configGun = Resources.Load<ConfigGun>("Configs/ConfigGun");
        yield return new WaitUntil(() => configGun != null);
        configEnemy = Resources.Load<ConfigEnemy>("Configs/ConfigEnemy");
        yield return new WaitUntil(() => configEnemy != null);
        configCharacter = Resources.Load<ConfigCharacter>("Configs/ConfigCharacter");
        yield return new WaitUntil(() => configCharacter != null);
        configMission = Resources.Load<ConfigMission>("Configs/ConfigMission");
        yield return new WaitUntil(() => configMission != null);
        
        Debug.Log("Loaded config");
        
        yield return new WaitForSeconds(1f);
        if (callback != null)
        {
            callback.Invoke();
        }
        
    }
}
