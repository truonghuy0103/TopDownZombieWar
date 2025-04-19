using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WeaponBehaviour : MonoBehaviour
{
    public IWeapon iWeapon;

    public float rateOfFire;
    public float timeReload;
    public int clipSize;
    public int currentBullet;
    public int amountAmo;
    public int damage;

    [SerializeField] private GameObject _goMuzzle;

    private float _timer;

    private ConfigGunData _configGunData;
    private WeaponControl _weaponControl;

    [SerializeField] private AudioSource _soundWeapon;
    
    public virtual void OnSetupBehaviour(ConfigGunData configGunData, WeaponControl weaponControl)
    {
        _configGunData = configGunData;
        rateOfFire = _configGunData.rateOfFire;
        timeReload = _configGunData.timeReload;
        clipSize = _configGunData.clipSize;
        currentBullet = clipSize;
        amountAmo = _configGunData.amountAmo;
        damage = _configGunData.damage;

        _timer = 0;
        
        _weaponControl = weaponControl;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_weaponControl != null)
        {
            if (_weaponControl.IsFire && _timer >= rateOfFire)
            {
                //Play anim attack
                _weaponControl.PlayAttackAnimation();
                OnAttack();
                _timer = 0;
            }
        }
    }

    public void OnAttack()
    {
        if (currentBullet > 0)
        {
            iWeapon.OnAttack(this);
            _soundWeapon.Play();
            PlayMuzzle();
        }
        else
        {
            _weaponControl.OnReload();
        }
    }

    public void PlayMuzzle()
    {
        if (_goMuzzle != null && !_goMuzzle.activeSelf)
        {
            StopCoroutine(RunMuzzle());
            StartCoroutine(RunMuzzle());
        }
    }

    IEnumerator RunMuzzle()
    {
        _goMuzzle.SetActive(true);
        _goMuzzle.transform.localRotation = Quaternion.Euler(0, 90, Random.Range(0, 180));
        yield return new WaitForSeconds(0.1f);
        _goMuzzle.SetActive(false);
    }
}