using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    private int _damage;
    private float _speed;
    [SerializeField] private LayerMask _layerMask;
    private Vector3 _direction;
    private HashSet<GameObject> _hitEnemies = new HashSet<GameObject>();
    private float _lifeTime = 2f;

    public void OnShoot(float speed, Vector3 direction, int damage)
    {
        transform.forward = direction;
        _damage = damage;
        _speed = speed;
        _direction = direction;
        
        _hitEnemies.Clear();
    }
    
    private void OnEnable()
    {
        CancelInvoke(nameof(ReturnBulletToPool));
        Invoke(nameof(ReturnBulletToPool), _lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ReturnBulletToPool));
    }

    private void Update()
    {
        float moveDistance = _speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !_hitEnemies.Contains(other.gameObject))
        {
            _hitEnemies.Add(other.gameObject);

            other.GetComponent<ZombieOnDamage>()?.ApplyDamage(_damage);

            Transform impact = PoolManager.Instance.dictPools[NamePool.PoolImpactEnemy.ToString()].GetObjectInstance();
            impact.position = other.transform.position;
            impact.forward = other.transform.forward;
        }
    }

    private void ReturnBulletToPool()
    {
        PoolManager.Instance.dictPools[NamePool.PoolBulletShotgun.ToString()].DisableObjectPool(gameObject);
    }
}
