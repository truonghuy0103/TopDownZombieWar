using UnityEngine;
using System;

public class PoolDefine : SingletonMono<PoolDefine>
{
   public void InitPool(Action callback = null)
   {
      Transform transBulletAuto = Resources.Load<Transform>("Pools/BulletAuto") as Transform;
      CreatePool(NamePool.PoolBulletAuto,40,transBulletAuto);
      
      Transform transImpactEnemy = Resources.Load<Transform>("Pools/ImpactEnemy") as Transform;
      CreatePool(NamePool.PoolImpactEnemy,50,transImpactEnemy);
      
      Transform transBloodPlayer = Resources.Load<Transform>("Pools/BloodPlayer") as Transform;
      CreatePool(NamePool.PoolBloodPlayer,10,transBloodPlayer);
      
      Transform transBulletShotgun = Resources.Load<Transform>("Pools/BulletShotgun") as Transform;
      CreatePool(NamePool.PoolBulletShotgun,10,transBulletShotgun);
      
      Transform transGrenadeFrag = Resources.Load<Transform>("Pools/GrenadeFrag") as Transform;
      CreatePool(NamePool.PoolGrenadeFrag,2,transGrenadeFrag);
      
      Transform transFragExplosion = Resources.Load<Transform>("Pools/FragExplosion") as Transform;
      CreatePool(NamePool.PoolFragExplosion,2,transFragExplosion);
      
      Transform transImpactObstacle = Resources.Load<Transform>("Pools/ImpactObstacle") as Transform;
      CreatePool(NamePool.PoolImpactObstacle,30,transImpactObstacle);

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
   PoolBulletShotgun = 3,
   PoolGrenadeFrag = 4,
   PoolFragExplosion = 5,
   PoolImpactObstacle = 6
}
