using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName ="CharacterInfo",menuName ="Setting/CharacterInfo")]
    public class CharacterInfo : ScriptableObject
    {


        public UnArmAttackInfo unArmAttackInfo;
        [ReadOnly]
        public GunAttackInfo gunAttackInfo;

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

        public void CopyValue(CharacterInfo info)
        {
            unArmAttackInfo = info.unArmAttackInfo;

            gunAttackInfo = info.gunAttackInfo;
        }
    }
}