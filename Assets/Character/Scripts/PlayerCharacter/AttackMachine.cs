using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class AttackMachine : MonoBehaviour
    {

        public Transform shootPoint;
        public Transform r_hand;

        [HideInInspector]
        public Weaponsbase currentWeapons;

        [HideInInspector]
        public GameObject shootLinerObj;
        [ReadOnly]
        public WeaponsType weaponsType;
        [ReadOnly]
        public bool isAim;
        [ReadOnly]
        public UnArmAttackInfo unArmAttackInfo;
        [ReadOnly]
        public GunAttackInfo gunAttackInfo;

        Collider[] hitColliders=new Collider[1];
        RaycastHit shootResult;
        CharacterInfo defaultCharacterInfo;


        public void SetWeapons<T>(T obj) where T:Weaponsbase
        {
            currentWeapons = obj;
            if (currentWeapons == null)
            {
                weaponsType = WeaponsType.none;
                shootLinerObj = null;
                return;
            }
            if(typeof(T)==typeof(Weaponsbase))
            {
                var t = obj as Weaponsbase;
                weaponsType = t.weaponsType;
                shootLinerObj = t.bulletLinerObj;
                shootPoint.localPosition = t.shootPoint;
            }

        }
        public void AddAttack()
        {

        }

        public void FistAttack()
        {
        
            var number = Physics.OverlapSphereNonAlloc(r_hand.position, unArmAttackInfo.fistAttackCheckRange, hitColliders, (1 << 9 | 1 << 10));
            if (number>0)
            {
                var t= hitColliders[0].GetComponent<IDestructible>();
                if(t!=null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = unArmAttackInfo.fistAttackDamage;
                    t.Attack(attackInfo);
                }
            }
        }

        public void GunAttack()
        {
            Vector3 shootDir = transform.forward;

            float offset = isAim ? gunAttackInfo.aimAttackOffset : gunAttackInfo.attackOffset;
            Random.InitState(RandomSeed.GetSeed());
            float angle= Random.Range(-offset, offset);
            var qa= Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa*shootDir;
            float dis = isAim ? gunAttackInfo.aimAttackDistance : gunAttackInfo.attackDistance;
            bool isHit = Physics.Raycast(shootPoint.position, shootDir, out shootResult,dis, (1 << 9|1<<10));
            GameObject obj = Instantiate(shootLinerObj);
            obj.transform.SetParent(shootPoint);
            obj.transform.position = new Vector3(0, 0, 0);
            if (isHit)
            {
                obj.GetComponent<BulletLiner>().SetPoint(shootPoint.position, shootResult.point);

                var t= shootResult.transform.GetComponent<IDestructible>();
                if(t!=null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = gunAttackInfo.attackDamage;
                    t.Attack(attackInfo);
                }
            }else
            {
                Vector3 end = shootPoint.position + shootDir * dis;
                obj.GetComponent<BulletLiner>().SetPoint(shootPoint.position, end);
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
           
        }
#endif
    }

   
}