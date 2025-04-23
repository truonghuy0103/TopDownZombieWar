using UnityEngine;

public class AutoAction : IWeapon
{
    private AutoWeapon _autoWeapon;
    public void OnAttack(object data)
    {
       _autoWeapon = data as AutoWeapon;
       SoundManager.Instance.PlaySoundSFX(_autoWeapon.soundFire,0.1f);
       //Create bullet
       Transform transBullet = PoolManager.Instance.dictPools[NamePool.PoolBulletAuto.ToString()].GetObjectInstance();
       transBullet.position = _autoWeapon.posShoot.position;

       Vector3 dir = _autoWeapon.aimPosShoot.position - _autoWeapon.posShoot.position;
       dir.y = 0;
       dir.Normalize();
       transBullet.up = dir;
       transBullet.GetComponent<AutoBullet>().OnShoot(_autoWeapon.speed, dir, _autoWeapon.damage);
       
       _autoWeapon.currentBullet--;
    }

    public void OnReload(object data)
    {
        _autoWeapon = data as AutoWeapon;
        int bulletsNeeded = _autoWeapon.clipSize - _autoWeapon.currentBullet;
        if (_autoWeapon.amountAmo >= _autoWeapon.clipSize)
        {
            _autoWeapon.currentBullet += bulletsNeeded;
            _autoWeapon.amountAmo -= bulletsNeeded;
        }
        else
        {
            _autoWeapon.currentBullet += _autoWeapon.amountAmo;
            _autoWeapon.amountAmo = 0;
        }
    }
}
