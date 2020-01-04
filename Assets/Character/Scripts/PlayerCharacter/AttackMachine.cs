using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class AttackMachine : MonoBehaviour
    {
        public Vector3 shootPoint;
        //public Transform r_hand;
        [HideInInspector]
        public bool playDefault = true;


        //[ReadOnly]
        //public bool isAim;
        //[ReadOnly]
        //public WeaponsAttackInfo weaponsAttackInfo;

        //public event AttackEvent ReloadBullets;

        //public AttackCommand DefaultCommand
        //{
        //    get { return defaultCommand; }
        //    protected set { defaultCommand = value; }
        //}

        //public AttackCommand CurrentCommand
        //{
        //    get { return currentCommand; }
        //    protected set { currentCommand = value; }
        //}

        public IAttack DefaultAttack
        {
            get { return defaultAttack; }
             set { defaultAttack = value; }
        }
        public IAttack CurrentAttack
        {
            get { return currentAttack; }
             set { currentAttack = value; }
        }


        private IAttack defaultAttack;
        private IAttack currentAttack;

        //private AttackCommand defaultCommand;
        //private AttackCommand currentCommand;

        bool isAttack;

        public event Action StopAttackEvent;
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

        //public void SetDefaultCommand(AttackCommand attackCommand)
        //{
        //    DefaultCommand = attackCommand;
        //}

        //public void SetAttackCommand(AttackCommand attackCommand)
        //{
        //    CurrentCommand = attackCommand;
        //}
        public void StartAttack()
        {
            isAttack = true;
        }
        public void StopAttack()
        {
            isAttack = false;
            StopAttackEvent?.Invoke();
        }

        public void Attack(AttackStage attackStage)
        {
            //AttackCommand c = playDefault ? DefaultCommand : CurrentCommand;
            if (!isAttack)
                return;
            IAttack a = playDefault ? DefaultAttack : CurrentAttack;


            switch (attackStage)
            {
                case AttackStage.enter:
                   //c.Prepare(this);
                    a.AttackPrepare(this);
                    break;
                case AttackStage.start:
                    //c.Start();
                    a.AttackStart();
                    break;
                case AttackStage.update:
                    //c.Update();
                    a.AttackUpdate();
                    break;
                case AttackStage.tick:
                    //c.Tick();
                    a.AttackTick();
                    break;
                case AttackStage.end:
                    //c.End();
                    a.AttackEnd();
                    break;
                case AttackStage.exit:
                    //c.Exit();
                    a.AttackExit();
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