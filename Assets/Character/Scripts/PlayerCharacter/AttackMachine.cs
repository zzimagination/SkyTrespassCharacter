using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class AttackMachine : MonoBehaviour
    {

        public Transform center;
        public Transform r_hand;

        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Transform shootPoint;
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
                shootPoint = null;
                attackCD = 0;
                attackMaxDistance = 0;
                return;
            }
            weaponsType = w.weaponsType;
            shootPoint = w.shootPoint;
            shootLinerObj = w.bulletLinerObj;
            attackCD = w.attackCD;
            attackMaxDistance = w.attackDistance;
        }


        public void FistAttack()
        {

            var number = Physics.OverlapSphereNonAlloc(r_hand.position, _interlfistAttackRange, colliders, (1 << 9 | 1 << 10));
            if (number>0)
            {
                Debug.Log(colliders[0].name);
            }
        }

        public void GunAttack()
        {
            bool isHit = Physics.Raycast(shootPoint.position, transform.forward, out shootResult, attackMaxDistance, (1 << 9|1<<10));
            GameObject obj = Instantiate(shootLinerObj);
            obj.transform.SetParent(currentWeapons.transform);
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

        bool isDraw;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            ////Check if there has been a hit yet
            //if (hitDetect)
            //{
            //    //Draw a Ray forward from GameObject toward the hit
            //    Gizmos.DrawRay(transform.position, transform.forward * hitResult.distance);
            //    //Draw a cube that extends to where the hit exists
            //    Gizmos.DrawWireCube(transform.position + transform.forward * hitResult.distance, boxSize);
            //}
            ////If there hasn't been a hit yet, draw the ray at the maximum distance
            //else
            //{
            //    //Draw a Ray forward from GameObject toward the maximum distance
            //Gizmos.DrawRay(shootPoint.position, transform.forward, );
            //    //Draw a cube at the maximum distance
            //    Gizmos.DrawWireCube(transform.position + transform.forward , boxSize);
            //}

        }
#endif
    }
}