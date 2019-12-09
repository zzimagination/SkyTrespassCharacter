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

        AttackCommand currentCommand;
        Weaponsbase currentWeapons;

        public void SetWeapons(Weaponsbase obj)
        {
            if (obj == null)
            {
                currentCommand = new UnarmAttackCommand();
                shootPoint = null;
            }else
            {
                currentCommand = obj.attackCommand;
            } 
        }

        public void Attack(AttackStage attackStage)
        {
            if (attackStage == AttackStage.enter)
            {
                currentCommand.Prepare(this);
            }
            else if (attackStage == AttackStage.start)
            {
                currentCommand.Start();
            }
            else if (attackStage == AttackStage.keep)
            {
                currentCommand.Keep();

            }
            else if (attackStage == AttackStage.end)
            {
                currentCommand.End();
            }
        }

    }

    public class UnarmAttackCommand : AttackCommand
    {
        public Transform r_hand;
        public UnArmAttackInfo unArmAttackInfo;

        public override void Prepare(AttackMachine attackMachine)
        {
            unArmAttackInfo = attackMachine.unArmAttackInfo;
            r_hand = attackMachine.r_hand;
        }
        public override void Start()
        {

        }
        public override void Keep()
        {
            float Range = unArmAttackInfo.attackCheckRange * unArmAttackInfo.attackCheckRange_Per;
            float damage = unArmAttackInfo.damage * unArmAttackInfo.damage_Per;
           
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
        public override void End()
        {

        }
    }

}