using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName = "WeaponsInfo", menuName = "Setting/WeaponsInfo")]
    public class WeaponsInfo : ScriptableObject
    {

        public float damage;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float CD;
        public float Distance;
        public float offset;
        public bool hasAim;

        [ShowIf("hasAim")]
        public float aimDamage;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        [ShowIf("hasAim")]
        public float aimCD;
        [ShowIf("hasAim")]
        public float aimDistance;
        [ShowIf("hasAim")]
        public float aimOffset;
    }

}