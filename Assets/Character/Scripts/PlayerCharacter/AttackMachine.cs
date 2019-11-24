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
        //[ReadOnly]
        //[BoxGroup("Attack Info")]
        //public float attackDamage;
        //[ReadOnly]
        //[BoxGroup("Attack Info")]
        //public float attackDistance;
        //[ReadOnly]
        //[BoxGroup("Attack Info")]
        //public float attackOffset;
        //[ReadOnly]
        //[BoxGroup("Attack Info")]
        //public float fistAttackRange;
        //[ReadOnly]
        //[ShowIfGroup("isAim")]
        //[BoxGroup("isAim/Aim")]
        //public float aimAttackDamage;
        //[ReadOnly]
        //[BoxGroup("isAim/Aim")]
        //public float aimAttackDistance;
        //[ReadOnly]
        //[BoxGroup("isAim/Aim")]
        //public float aimAttackOffset;

        Collider[] colliders=new Collider[1];
        RaycastHit shootResult;
        CharacterInfo defaultCharacterInfo;


        public void SetWeapons(Weaponsbase w)
        {
            currentWeapons = w;
            if (w == null)
            {
                weaponsType = WeaponsType.none;
                shootLinerObj = null;
                return;
            }
            weaponsType = w.weaponsType;
            shootLinerObj = w.bulletLinerObj;
            shootPoint.localPosition = w.shootPoint;
        }
        public void AddAttack()
        {

        }

        public void FistAttack()
        {
        
            var number = Physics.OverlapSphereNonAlloc(r_hand.position, unArmAttackInfo.fistAttackCheckRange, colliders, (1 << 9 | 1 << 10));
            if (number>0)
            {

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

    [System.Serializable]
    public struct GunAttackInfo
    {
        [BoxGroup("Attack Info")]
        public float attackDamage;
        [BoxGroup("Attack Info")]
        public float attackCD;
        [BoxGroup("Attack Info")]
        public float attackDistance;
        [BoxGroup("Attack Info")]
        public float attackOffset;

        public bool hasAim;

        [ShowIfGroup("hasAim")]
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackDamage;
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackCD;
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackDistance;
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackOffset;
    }
    [System.Serializable]
    public struct UnArmAttackInfo
    {
        [BoxGroup("Fist")]
        public float fistAttackDamage;
        [BoxGroup("Fist")]
        public float fistAttackCD;
        [BoxGroup("Fist")]
        public float fistAttackCheckRange;
    }
}