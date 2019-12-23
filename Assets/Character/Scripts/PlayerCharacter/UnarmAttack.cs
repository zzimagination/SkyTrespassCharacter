using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class UnarmAttack : IAttack
    {
        public Transform r_hand;
        public CharacterAttackInfo unArmAttackInfo;
        public float unarmDamage;
        public float unarmAttackCheckRange;


        public void End()
        {
          
        }

        public void Exit()
        {
            
        }

        public void Prepare(AttackMachine attackMachine)
        {
           
        }

        public void Start()
        {
           
        }

        public void Tick()
        {

            var number = Physics.OverlapSphere(r_hand.position, unarmAttackCheckRange, (1 << 9 | 1 << 10));
            if (number.Length > 0)
            {
                var t = number[0].GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = unarmDamage;
                    t.Attack(attackInfo);
                }
            }
        }

        public void Update()
        {
            
        }
    }
}