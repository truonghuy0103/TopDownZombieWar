using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    private int _damage;
    private float _speed;
    [SerializeField] private LayerMask _layerMask;
    private Vector3 _direction;

    public void OnShoot(float speed, Vector3 direction, int damage)
    {
        transform.forward = direction;
        _damage = damage;
        _speed = speed;
        _direction = direction;
    }

    private void Update()
    {
        float moveDistance = _speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, _direction, moveDistance + 0.1f);
        if (hits != null && hits.Length > 0)
        {
            OnHit(hits);
        }
    }

    private void OnHit(RaycastHit[] hitInfo)
    {
        foreach (var hit in hitInfo)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<ZombieOnDamage>()?.ApplyDamage(_damage);
                
                Transform impact = PoolManager.Instance.dictPools[NamePool.PoolImpactEnemy.ToString()].GetObjectInstance();
                impact.position = hit.point;
                impact.forward = hit.normal;
            }
        }
        
        //PoolManager.Instance.dictPools[NamePool.PoolBulletShotgun.ToString()].DisableObjectPool(gameObject);
    }
}
