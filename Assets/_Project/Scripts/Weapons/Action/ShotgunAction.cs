using UnityEngine;

public class ShotgunAction : IWeapon
{
    private ShotgunWeapon _shotgunWeapon;
    public void OnAttack(object data)
    {
        _shotgunWeapon = data as ShotgunWeapon;
        //Create bullet
        Transform transBullet = PoolManager.Instance.dictPools[NamePool.PoolBulletShotgun.ToString()].GetObjectInstance();
        transBullet.position = _shotgunWeapon.posShoot.position;

        Vector3 dir = _shotgunWeapon.aimPosShoot.position - _shotgunWeapon.posShoot.position;
        dir.y = 0;
        dir.Normalize();
        transBullet.up = dir;
        transBullet.GetComponent<ShotgunBullet>().OnShoot(_shotgunWeapon.speed, dir, _shotgunWeapon.damage);
       
        _shotgunWeapon.currentBullet--;
    }

    public void OnReload(object data)
    {
        _shotgunWeapon = data as ShotgunWeapon;
        int bulletsNeeded = _shotgunWeapon.clipSize - _shotgunWeapon.currentBullet;
        if (_shotgunWeapon.amountAmo >= _shotgunWeapon.clipSize)
        {
            _shotgunWeapon.currentBullet += bulletsNeeded;
            _shotgunWeapon.amountAmo -= bulletsNeeded;
        }
        else
        {
            _shotgunWeapon.currentBullet += _shotgunWeapon.amountAmo;
            _shotgunWeapon.amountAmo = 0;
        }
    }
}
