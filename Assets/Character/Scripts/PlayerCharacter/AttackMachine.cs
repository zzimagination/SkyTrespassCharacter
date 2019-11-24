using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [HideInInspector]
        public WeaponsType weaponsType;
        [HideInInspector]
        public float attackCD;
        [HideInInspector]
        public float attackMaxDistance;

        public readonly float fistAttackCD = 0.4f;

        Collider[] colliders=new Collider[1];
        RaycastHit shootResult;

        const float _interlfistAttackRange=0.3f;


        public void SetWeapons(Weaponsbase w)
        {
            currentWeapons = w;
            if (w == null)
            {
                weaponsType = WeaponsType.none;
                shootLinerObj = null;
                attackCD = 0;
                attackMaxDistance = 0;
                return;
            }
            weaponsType = w.weaponsType;
            shootLinerObj = w.bulletLinerObj;
            attackCD = w.attackCD;
            attackMaxDistance = w.attackDistance;

            shootPoint.localPosition = w.shootPoint;
        }


        public void FistAttack()
        {

            var number = Physics.OverlapSphereNonAlloc(r_hand.position, _interlfistAttackRange, colliders, (1 << 9 | 1 << 10));
            if (number>0)
            {

            }
        }

        public void GunAttack()
        {
            bool isHit = Physics.Raycast(shootPoint.position, transform.forward, out shootResult, attackMaxDistance, (1 << 9|1<<10));
            GameObject obj = Instantiate(shootLinerObj);
            obj.transform.SetParent(shootPoint);
            obj.transform.position = new Vector3(0, 0, 0);
            if (isHit)
            {
                obj.GetComponent<BulletLiner>().SetPoint(shootPoint.position, shootResult.point);
            }else
            {
                Vector3 end = shootPoint.position + transform.forward * attackMaxDistance;
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