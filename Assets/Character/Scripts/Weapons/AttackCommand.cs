using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public abstract class AttackCommand
    {
        public delegate void AttackEvent();

        public virtual void Prepare(AttackMachine attackMachine) { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Tick() { }
        public virtual void End() { }
        public virtual void Exit() { }
    }


}