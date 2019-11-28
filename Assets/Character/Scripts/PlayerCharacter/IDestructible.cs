using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass
{
    interface IDestructible
    {
        void Attack(AttackInfo attackInfo);
        void Death();
    }


    public struct AttackInfo
    {
        public float damage;

    }
}