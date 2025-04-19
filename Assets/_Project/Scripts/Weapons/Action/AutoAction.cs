using UnityEngine;

public class AutoAction : IWeapon
{
    private AutoWeapon _autoWeapon;
    public void OnAttack(object data)
    {
       _autoWeapon = data as AutoWeapon;
       //Create bullet

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
