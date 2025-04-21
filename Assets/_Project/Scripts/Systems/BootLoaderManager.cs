using System;
using System.Collections;
using Core;
using UnityEngine;

public class BootLoaderManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        ConfigManager.Instance.InitConfig(() =>
        {
            UIManager.Instance.Init(() =>
            {
                UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
            });
        });
    }
}
