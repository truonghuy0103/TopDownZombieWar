using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using System.Collections;
using Core;

public class UIInGame : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private WeaponControl _weaponControl;

    [SerializeField] private GameObject _reloadContent;

    [SerializeField] private Image _imgReloadProgress;
    [SerializeField] private Image _imgHPProgress;

    [SerializeField] private TMP_Text _txtTimeReload;
    [SerializeField] private TMP_Text _txtAmo;
    [SerializeField] private TMP_Text _txtTimeRemain;
    
    private WeaponBehaviour _weaponBehaviour;

    private float _timeRemain;
    
    public void SetupUIInGame()
    {
        _playerHealth.OnHPChanged -= HPChangeHandle;
        _playerHealth.OnHPChanged += HPChangeHandle;
        
        _weaponControl.OnChangeGunHandle -= ChangeGunHandle;
        _weaponControl.OnChangeGunHandle += ChangeGunHandle;
        _weaponControl.SwitchWeapon();
        _reloadContent.SetActive(false);
        
        int currentMission = GameManager.Instance.currentMission;
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(currentMission.ToString());
        _timeRemain = configMissionData.duration;
        
        StartCoroutine(UpdateTimeRemain());
    }
    
    private IEnumerator UpdateTimeRemain()
    {
        while (_timeRemain > 0)
        {
            _timeRemain -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_timeRemain / 60f);
            int seconds = Mathf.FloorToInt(_timeRemain % 60f);
            _txtTimeRemain.text = $"{minutes:0}:{seconds:00}";
            yield return null;
        }

        _timeRemain = 0;
        _txtTimeRemain.text = "0:00";
        UIManager.Instance.ShowUI(UIIndex.UIWin);
    }

    private void OnReloadHandle(float timeReload, Action callback)
    {
        float fillAmount = 0;
        _reloadContent.SetActive(true);
        DOTween.To(() => fillAmount, x => fillAmount = x, 1, timeReload)
            .OnComplete(() => { _reloadContent.SetActive(false); }).OnUpdate(() =>
            {
                _imgReloadProgress.fillAmount = fillAmount;
            });

        float timeCount = timeReload;
        DOTween.To(() => timeCount, x => timeCount = x, 0, timeReload).OnUpdate(() =>
        {
            _txtTimeReload.text = timeCount.ToString("F1");
        });
    }

    public void UpdateBulletHandle(int current, int amo)
    {
        _txtAmo.text = current.ToString() + "/" + amo.ToString();
    }

    private void HPChangeHandle(int curHP, int maxHP)
    {
        _imgHPProgress.fillAmount = (float)curHP / maxHP;
        if (curHP <= 0)
        {
            StopCoroutine(UpdateTimeRemain());
            UIManager.Instance.ShowUI(UIIndex.UILose);
        }
    }

    public void ChangeGunHandle(WeaponBehaviour weaponBehaviour)
    {
        _weaponBehaviour = weaponBehaviour;
        if (_weaponBehaviour.amountAmo > 0)
        {
            _txtAmo.text = _weaponBehaviour.currentBullet.ToString() + "/" + _weaponBehaviour.amountAmo.ToString();
        }
        else
        {
            _txtAmo.text = "0/0";
        }

        _weaponControl.OnUpdateBulletHandle -= UpdateBulletHandle;
        _weaponControl.OnUpdateBulletHandle += UpdateBulletHandle;

        _weaponControl.OnReloadHandle -= OnReloadHandle;
        _weaponControl.OnReloadHandle += OnReloadHandle;
    }

    public void ButtonSwitchWeaponClicked()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.SwitchWeapon);
        _weaponControl.SwitchWeapon();
    }

    public void PauseButtonClicked()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        UIManager.Instance.ShowUI(UIIndex.UIPause);
    }

}
