using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName ="WeaponsInfo",menuName ="Setting/WeaponsInfo")]
    public class WeaponsInfo : ScriptableObject
    {

        public GunAttackInfo attackInfo;
        //public bool hasAim;

       
        //public float attackDamage;
        //public float attackCD;
        //public float attackDistance;
        //public float attackOffset;
        //[ShowIfGroup("hasAim")]
        //[BoxGroup("hasAim/AimAttack")]
        //public float aimAttackDamage;
        //[BoxGroup("hasAim/AimAttack")]
        //public float aimAttackCD;
        //[BoxGroup("hasAim/AimAttack")]
        //public float aimAttackDistance;
        //[BoxGroup("hasAim/AimAttack")]
        //public float aimAttackOffset;
    }
}