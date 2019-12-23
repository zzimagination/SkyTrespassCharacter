using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public abstract class Weaponsbase : MonoBehaviour,IAttack
    {

        private int attackNumber = -1;

        public WeaponsType weaponsType;
        public delegate void AttackEvent();
        public int AttackNumber
        {
            get
            {
                return attackNumber;
            }
            protected set { attackNumber = value; }
        }
        public virtual void Hidden()
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
        public virtual int DoAttackNumber() { return 0; }
        public virtual int ResetAttackNumber() { return 0; }
        public virtual int MaxAttackNumber() { return 0; }
        public virtual AttackCommand CreatAttackCommand() { return null; }
        public abstract void AddCharacterInfo(CharacterAttackInfo characterInfo);
        public abstract void SubCharacterInfo(CharacterAttackInfo characterInfo);

        public virtual void Prepare(AttackMachine attackMachine)
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Tick()
        {
        }

        public virtual void End()
        {
        }

        public virtual void Exit()
        {
        }
    }
}