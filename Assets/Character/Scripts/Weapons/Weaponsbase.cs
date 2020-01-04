using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public abstract class Weaponsbase : MonoBehaviour,IAttack
    {
        public WeaponsType weaponsType;

        public int magazineCapacity;
        public Transform leftHandIK;
        public BulletLinerPool linerPool;
        [HideInInspector]
        public AttackMachine attackMachine;

        public int RemainBullet
        {
            get { return remainBullet; }
            protected set
            {
                remainBullet = value;
            }
        }

        [SerializeField]
        private int remainBullet;

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        public virtual void Drop()
        {
            Destroy(gameObject);
        }

        public virtual int ReloadBullet(int standby)
        {
            return standby;
        }
        public virtual void ChangeAim(bool isAim) { }


        public abstract void InitWeapons();

        public virtual void AttackPrepare(AttackMachine attackMachine)
        {
        }

        public virtual void AttackStart()
        {
        }

        public virtual void AttackUpdate()
        {
        }

        public virtual void AttackTick()
        {
        }

        public virtual void AttackEnd()
        {
        }

        public virtual void AttackExit()
        {
        }
    }
}