using UnityEngine;

public class FragGrenade : MonoBehaviour
{
    public int damage = 20;
    public float delay = 2f;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    
    public void WaitingForGrenadeDamage()
    {
        Invoke(nameof(GrenadeDamage), delay);
    }

    private void GrenadeDamage()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.FragGrenadeExplode);
        
        Transform transExplosion = PoolManager.Instance.dictPools[NamePool.PoolFragExplosion.ToString()].GetObjectInstance();
        transExplosion.position = transform.position;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.isTrigger = false;
                
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
                
                collider.GetComponent<ZombieOnDamage>()?.ApplyDamage(damage);
                collider.isTrigger = true;
                
                Transform impact = PoolManager.Instance.dictPools[NamePool.PoolImpactEnemy.ToString()].GetObjectInstance();
                impact.position = collider.transform.position;
                impact.forward = collider.transform.forward;
            }
        }
        
        //PoolManager.Instance.dictPools[NamePool.PoolFragExplosion.ToString()].DisableObjectPool(transExplosion.gameObject);
        PoolManager.Instance.dictPools[NamePool.PoolGrenadeFrag.ToString()].DisableObjectPool(gameObject);
    }
}
