using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class AttackMachine : MonoBehaviour
    {
        public Transform r_hand;

        [ReadOnly]
        public bool isAim;
        [ReadOnly]
        public WeaponsAttackInfo weaponsAttackInfo;

        public event AttackEvent ReloadBullets;

        AttackCommand currentCommand;
        Weaponsbase currentWeapons;

        int attackNumber;
        public delegate void AttackEvent();

        public void SetWeapons(Weaponsbase obj)
        {
            currentWeapons = obj;

            if (obj == null)
            {
                currentCommand = new UnarmAttackCommand();
            }else
            {
                currentCommand = obj.attackCommand;
            }
            attackNumber = 0;
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
                attackNumber++;
            }
            else if (attackStage == AttackStage.end)
            {
                currentCommand.End();
                if (attackNumber >= weaponsAttackInfo.magazineCapacity)
                {
                    attackNumber = 0;
                    ReloadBullets?.Invoke();
                }
            }else if(attackStage== AttackStage.exit)
            {

            }
        }

    }

    public class UnarmAttackCommand : AttackCommand
    {
        public Transform r_hand;
        public WeaponsAttackInfo unArmAttackInfo;

        public override void Prepare(AttackMachine attackMachine)
        {
            unArmAttackInfo = attackMachine.weaponsAttackInfo;
            r_hand = attackMachine.r_hand;
        }
        public override void Start()
        {

        }
        public override void Keep()
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
        public override void End()
        {

        }
    }

}