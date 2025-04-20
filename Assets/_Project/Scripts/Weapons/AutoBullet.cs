using Unity.VisualScripting;
using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    private int _damage;
    private float _speed;
    [SerializeField] private LayerMask _layerMask;
    private Vector3 _direction;
    private RaycastHit _hit;

    public void OnShoot(float speed, Vector3 direction, int damage)
    {
        transform.forward = direction;
        _damage = damage;
        _speed = speed;
        _direction = direction;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        Physics.Raycast(transform.position, _direction, out _hit, 0.5f);
        if (_hit.collider != null)
        {
            Debug.Log("Hit: " + _hit.collider.name);
            OnHit(_hit);
        }
    }

    private void OnHit(RaycastHit hitInfo)
    {
        Transform impact = null;
        //Damage Enemy
        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
        {
            hitInfo.collider.gameObject.GetComponent<ZombieOnDamage>().ApplyDamage(_damage);
            impact = PoolManager.Instance.dictPools[NamePool.PoolImpactEnemy.ToString()].GetObjectInstance();
        }
        
        if (impact != null)
        {
            impact.position = _hit.point;
            impact.forward = _hit.normal;
            PoolManager.Instance.dictPools[NamePool.PoolBulletAuto.ToString()].DisableObjectPool(gameObject);
        }
    }
    
}
