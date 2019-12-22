using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkyTrespass.Character
{
    public class UnarmAttackCommand : AttackCommand
    {
        public Transform r_hand;
        public CharacterAttackInfo unArmAttackInfo;

        public override void Tick()
        {
            float Range = unArmAttackInfo.unarmAttackCheckRange * unArmAttackInfo.unarmAttackCheckRange_Per;
            float damage = unArmAttackInfo.unarmDamage * unArmAttackInfo.unarmDamage_Per;

            var number = Physics.OverlapSphere(r_hand.position, Range, (1 << 9 | 1 << 10));
            if (number.Length > 0)
            {
                var t = number[0].GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = damage;
                    t.Attack(attackInfo);
                }
            }
        }
    }
}