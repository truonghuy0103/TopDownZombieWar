using UnityEngine;
using System;

public class PoolDefine : SingletonMono<PoolDefine>
{
   public void InitPool(Action callback = null)
   {
      Transform transBulletAuto = Resources.Load<Transform>("Pools/BulletAuto") as Transform;
      CreatePool(NamePool.PoolBulletAuto,50,transBulletAuto);
      
      Transform transImpactEnemy = Resources.Load<Transform>("Pools/ImpactEnemy") as Transform;
      CreatePool(NamePool.PoolImpactEnemy,50,transImpactEnemy);
      
      Transform transBloodPlayer = Resources.Load<Transform>("Pools/BloodPlayer") as Transform;
      CreatePool(NamePool.PoolBloodPlayer,10,transBloodPlayer);

      if (callback != null)
      {
         callback.Invoke();
      }
   }

   private void CreatePool(NamePool namePool, int maxObject, Transform prefabObjects)
   {
      PoolItem poolItem = new PoolItem();
      poolItem.namePool = namePool.ToString();
      poolItem.maxObject = maxObject;
      poolItem.prefab = prefabObjects;
      PoolManager.Instance.AddNewPool(poolItem);
   }
}

public enum NamePool
{
   PoolBulletAuto = 0,
   PoolImpactEnemy = 1,
   PoolBloodPlayer = 2,
}
