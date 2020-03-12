using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class WeaponsFist : Weaponsbase
    {
        public Transform r_hand;
        public int unarmDamage;
        public float unarmAttackCheckRange;
        List<IDestructible> targetList = new List<IDestructible>();
        public override void Close()
        {

        }
        public override void Open()
        {

        }
        public override void Drop()
        {

        }
        private void Awake()
        {
            magazineCapacity = 999;
            RemainBullet = 999;
        }

        public override void AttackEnd()
        {
            targetList.Clear();
        }

        public override void AttackUpdate()
        {
            var number = Physics.OverlapSphere(r_hand.position, unarmAttackCheckRange, (1 << 9 | 1 << 10));

            for (int i = 0; i < number.Length; i++)
            {
                var t = number[0].GetComponent<IDestructible>();
                if (t != null)
                {
                    if (!targetList.Contains(t))
                    {
                        targetList.Add(t);
                        AttackInfo attackInfo = new AttackInfo();
                        attackInfo.damage = unarmDamage;
                        t.Attack(attackInfo);
                    }
                }
            }

        }
    }
}