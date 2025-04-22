using UnityEngine;

public class ShotgunWeapon : WeaponBehaviour
{
    public Transform posShoot;
    public Transform aimPosShoot;

    public override void OnSetupBehaviour(ConfigGunData configGunData, WeaponControl weaponControl)
    {
        base.OnSetupBehaviour(configGunData, weaponControl);
        this.iWeapon = new ShotgunAction();
    }
}
