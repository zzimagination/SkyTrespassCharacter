using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class WeaponsRifle : Weaponsbase
    {

        public int shootDamage;
        public float shootOffset;
        public float shootDistance;
        public float shootAimOffset;
        private void Awake()
        {
            RemainBullet = magazineCapacity;
        }


        public override void InitWeapons()
        {
            RemainBullet = magazineCapacity;
        }

        public override int ReloadBullet(int standby)
        {
            int n = magazineCapacity - RemainBullet;
            if (n < 0)
                throw new System.Exception("换弹出错");
            if (standby >= n)
            {
                RemainBullet = magazineCapacity;
                return standby - n;
            }
            else
            {
                RemainBullet = RemainBullet + standby;
                return 0;
            }
        }

        public override void ChangeAim(bool aim)
        {
            this.isAim = aim;
        }

        public override void AttackTick()
        {
            var shootPosition =  playerTransform.position+new Vector3(0,1.4f,0);
            var shootDir = playerTransform.forward;

            int damage = shootDamage;
            float distance = shootDistance;
            float offset = isAim ? shootAimOffset : shootOffset;

            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, distance, (1 << 9 | 1 << 10));

            if (isHit)
            {
 
                var t = shootResult.transform.GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = damage;
                    t.Attack(attackInfo);
                }
                linerPool.CreatLiner(shootPosition, shootResult.point,playerTransform.rotation);
            }
            else
            {
                Vector3 end = shootPosition + shootDir * distance;
                linerPool.CreatLiner(shootPosition, end,playerTransform.rotation);
            }
            RemainBullet--;
        }

    }


   
}