using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public delegate void ChangeGunHandle(WeaponBehaviour weaponBehaviour);

public delegate void UpdateBulletHandle(int currentNumber, int total);

public delegate void ReloadHandle(float timeReload, Action callback);
public class WeaponControl : MonoBehaviour
{
    public event ChangeGunHandle OnChangeGunHandle;
    public event UpdateBulletHandle OnUpdateBulletHandle;
    public event ReloadHandle OnReloadHandle;
    
    public float rateOfFire = 0.5f;
    private float _timer = 0;
    
    [SerializeField] private List<WeaponBehaviour> _listWeapons = new List<WeaponBehaviour>();
    [SerializeField] private PlayerDataBinding _playerDataBinding;
    [SerializeField] private PlayerInput _playerInput;
    
    private WeaponBehaviour _currentWeapon;

    private bool _isFire;

    public bool IsFire
    {
        get => _isFire;
        set => _isFire = value;
    }

    private int _indexWeapon;
    
    public void SetupEventWeapon()
    {
        _playerInput.OnFire -= OnCheckFire;
        _playerInput.OnFire += OnCheckFire;

        OnReloadHandle -= OnReloadHandleEvent;
        OnReloadHandle += OnReloadHandleEvent;
    }

    public void PlayAttackAnimation()
    {
        _playerDataBinding.Attack = true;
    }

    public void OnReload()
    {
        _playerInput.OnFire -= OnCheckFire;
        IsFire = false;
        if (_currentWeapon.amountAmo > 0)
        {
            if (_currentWeapon.timeReload > 0)
            {
                OnReloadHandle(_currentWeapon.timeReload, () =>
                {
                    _currentWeapon.iWeapon.OnReload(_currentWeapon);
                    OnUpdateBullet();
                    _playerInput.OnFire += OnCheckFire;
                });
            }
            else
            {
                _currentWeapon.iWeapon.OnReload(_currentWeapon);
                OnUpdateBullet();
                _playerInput.OnFire += OnCheckFire;
            }
        }
    }
    
    public void OnReloadHandleEvent(float timer, Action callback)
    {
        float timeCountdown = 0;
        DOTween.To(() => timeCountdown, x => timeCountdown = x, timer, timer).OnComplete(() =>
        {
            if (callback != null)
            {
                callback.Invoke();
            }                
        });
    }

    private void OnCheckFire(bool isFire)
    {
        IsFire = isFire;
    }

    public void OnUpdateBullet()
    {
        if (OnUpdateBulletHandle != null)
        {
            OnUpdateBulletHandle.Invoke(_currentWeapon.currentBullet,_currentWeapon.amountAmo);
        }
    }

    public void SwitchWeapon()
    {
        _indexWeapon++;
        if (_indexWeapon >= _listWeapons.Count)
        {
            _indexWeapon = 0;
        }

        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
        }
        
        _listWeapons[_indexWeapon].gameObject.SetActive(true);
        _currentWeapon = _listWeapons[_indexWeapon];
        _currentWeapon.ResetMuzzle();
        
        if (OnChangeGunHandle != null)
        {
            OnChangeGunHandle.Invoke(_currentWeapon);
        }
    }

    public void SetupWeaponBehaviour()
    {
        for (int i = 0; i < _listWeapons.Count; i++)
        {
            int indexGun = i + 1;
            ConfigGunData configGunData = ConfigManager.Instance.configGun.GetGunById(indexGun.ToString());
            _listWeapons[i].OnSetupBehaviour(configGunData,this);
        }
        
        _indexWeapon = -1;
    }
}
