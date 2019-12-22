using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class AttackMachine : MonoBehaviour
    {
        //public Transform r_hand;
        public bool playDefault = true;
        //[ReadOnly]
        //public bool isAim;
        //[ReadOnly]
        //public WeaponsAttackInfo weaponsAttackInfo;

        //public event AttackEvent ReloadBullets;

        public AttackCommand DefaultCommand
        {
            get { return defaultCommand; }
            protected set { defaultCommand = value; }
        }

        public AttackCommand CurrentCommand
        {
            get { return currentCommand; }
            protected set { currentCommand = value; }
        }

        private AttackCommand defaultCommand;
        private AttackCommand currentCommand;
        //Weaponsbase currentWeapons;

        //int attackNumber;
        //public delegate void AttackEvent();

        //public void SetWeapons(Weaponsbase obj)
        //{
        //    currentWeapons = obj;

        //    if (obj == null)
        //    {
        //        currentCommand = new UnarmAttackCommand();
        //    }else
        //    {
        //        currentCommand = obj.attackCommand;
        //    }
        //    //attackNumber = 0;
        //}

        public void SetDefaultCommand(AttackCommand attackCommand)
        {
            DefaultCommand = attackCommand;
        }

        public void SetAttackCommand(AttackCommand attackCommand)
        {
            CurrentCommand = attackCommand;
        }

        public void Attack(AttackStage attackStage)
        {
            AttackCommand c = playDefault ? DefaultCommand : CurrentCommand;
            switch (attackStage)
            {
                case AttackStage.enter:
                    c.Prepare(this);
                    break;
                case AttackStage.start:
                    c.Start();
                    break;
                case AttackStage.update:
                    c.Update();
                    break;
                case AttackStage.tick:
                    c.Tick();
                    break;
                case AttackStage.end:
                    c.End();
                    break;
                case AttackStage.exit:
                    c.Exit();
                    break;
                default:
                    break;
            }

            //if (attackStage == AttackStage.enter)
            //{
            //    currentCommand.Prepare(this);

            //}
            //else if (attackStage == AttackStage.start)
            //{
            //    currentCommand.Start();
            //}
            //else if (attackStage == AttackStage.update)
            //{
            //    currentCommand.Update();
            //    //attackNumber++;
            //}
            //else if (attackStage == AttackStage.end)
            //{
            //    currentCommand.End();
            //    //if (attackNumber >= weaponsAttackInfo.magazineCapacity)
            //    //{
            //    //    attackNumber = 0;
            //    //    ReloadBullets?.Invoke();
            //    //}
            //}
            //else if (attackStage == AttackStage.exit)
            //{

            //}
        }

    }

}