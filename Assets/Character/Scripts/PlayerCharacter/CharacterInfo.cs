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


        public void CopyValue(CharacterInfo info)
        {
            unArmAttackInfo = info.unArmAttackInfo;

            gunAttackInfo = info.gunAttackInfo;
        }
    }


    [System.Serializable]
    public struct GunAttackInfo
    {
        [BoxGroup("Attack Info")]
        public float attackDamage;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        [BoxGroup("Attack Info")]
        public float attackCD;
        [BoxGroup("Attack Info")]
        public float attackDistance;
        [BoxGroup("Attack Info")]
        public float attackOffset;

        public bool hasAim;

        [ShowIfGroup("hasAim")]
        [Tooltip("武器动画片段的速度，1为默认速度")]
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackCD;
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackDistance;
        [BoxGroup("hasAim/AimAttack")]
        public float aimAttackOffset;
    }
    [System.Serializable]
    public struct UnArmAttackInfo
    {
        [BoxGroup("Fist")]
        public float fistAttackDamage;
        [BoxGroup("Fist")]
        public float fistAttackCD;
        [BoxGroup("Fist")]
        public float fistAttackCheckRange;
    }
}