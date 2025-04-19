using UnityEngine;

public interface IWeapon
{
    void OnAttack(object data);
    void OnReload(object data);
}
