using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public abstract class AttackCommand
    {
        public abstract void Prepare(AttackMachine attackMachine);
        public abstract void Start();
        public abstract void Keep();
        public abstract void End();
    }


}