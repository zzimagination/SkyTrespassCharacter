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

        [ReadOnly]
        public bool isAim;
        [ReadOnly]
        public UnArmAttackInfo unArmAttackInfo;
        [ReadOnly]
        public WeaponsAttackInfo weaponsAttackInfo;
        
        Collider[] hitColliders = new Collider[1];
        RaycastHit shootResult;

        AttackAction WeaponsAttack;
        delegate void AttackAction();



        public void SetWeapons(Weaponsbase obj)
        {
            if (obj==null)
            {
                return;
            }

            var w1 = obj as WeaponsRifle;
            if(w1)
            {
                shootPoint.localPosition = w1.shootLocalPoint;
                WeaponsAttack = () =>
                {
                    w1.isAimShoot = isAim;
                    w1.shootDir = transform.forward;
                    w1.shootPosition = shootPoint.position;
                    w1.Attack(weaponsAttackInfo);
                };
                return;
            }
            var w2 = obj as WeaponsPistol;
            if(w2)
            {
                shootPoint.localPosition = w2.shootLocalPoint;
                WeaponsAttack = () =>
                {
                    w2.isAimShoot = isAim;
                    w2.shootDir = transform.forward;
                    w2.shootPosition = shootPoint.position;
                    w2.Attack(weaponsAttackInfo);
                };
                return;
            }

        }

        public void FistAttack()
        {
            float Range = unArmAttackInfo.attackCheckRange * unArmAttackInfo.attackCheckRange_Per;
            float damage = unArmAttackInfo.damage * unArmAttackInfo.damage_Per;

            var number = Physics.OverlapSphereNonAlloc(r_hand.position, Range, hitColliders, (1 << 9 | 1 << 10));
            if (number > 0)
            {
                var t = hitColliders[0].GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = damage;
                    t.Attack(attackInfo);
                }
            }
        }

        public void GunAttack()
        {
            WeaponsAttack?.Invoke();
        }

    }


}