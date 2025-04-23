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
        UIManager.Instance.Init(() =>
        {
            SoundManager.Instance.Init(() =>
            {
                UIManager.Instance.ShowUI(UIIndex.UILoading);
                ConfigManager.Instance.InitConfig(() =>
                {
                    UIManager.Instance.HideUI(UIIndex.UILoading);
                    UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
                });
            });
        });
        
    }
}
