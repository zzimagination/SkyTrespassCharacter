using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class UnarmAttack : IAttack
    {
        public Transform r_hand;
        public int unarmDamage;
        public float unarmAttackCheckRange;


        List<IDestructible> targetList=new List<IDestructible>();

        public void AttackEnd()
        {
            targetList.Clear();
        }

        public void AttackExit()
        {
            
        }

        public void AttackPrepare()
        {
           
        }

        public void AttackStart()
        {
           
        }

        public void AttackTick()
        {

        }

        public void AttackUpdate()
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