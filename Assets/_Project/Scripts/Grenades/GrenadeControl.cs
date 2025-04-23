using System;
using UnityEngine;

public class GrenadeControl : MonoBehaviour
{
    public event Action<float> OnGrenedaThrown;
    
    [SerializeField] private Transform _grenadeHolder;
    [SerializeField] private float _throwForce = 5;
    [SerializeField] private float _grenadeInverval = 10f;
    
    public void ThrowGrenade()
    {
        Transform grenade =  PoolManager.Instance.dictPools[NamePool.PoolGrenadeFrag.ToString()].GetObjectInstance();
        grenade.position = _grenadeHolder.position;
        
        Rigidbody grenadeRigid = grenade.GetComponent<Rigidbody>();
        FragGrenade fragGrenade = grenade.GetComponent<FragGrenade>();
        
        grenadeRigid.AddForce(_grenadeHolder.forward * _throwForce, ForceMode.VelocityChange);
        fragGrenade.WaitingForGrenadeDamage();

        if (OnGrenedaThrown != null)
        {
            OnGrenedaThrown.Invoke(_grenadeInverval);
        }
    }
}
