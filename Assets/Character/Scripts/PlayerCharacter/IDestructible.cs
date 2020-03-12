using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass
{
    interface IDestructible
    {
        void Attack(AttackInfo attackInfo);
    }


    public struct AttackInfo
    {
        public int damage;

    }
}