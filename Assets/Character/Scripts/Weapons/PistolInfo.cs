using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Setting/WeaponsInfo/Pistol`")]
    public class PistolInfo : WeaponsBaseInfo
    {
        public int magazineCapacity;

        public float shootDamage;
        public float shootDamage_Per = 1;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootCD = 1;
        public float shootCD_Per = 1;
        public float shootOffset;
        public float shootOffset_Per = 1;
        public float shootDistance;
        public float shootDistance_Per = 1;

    }
}