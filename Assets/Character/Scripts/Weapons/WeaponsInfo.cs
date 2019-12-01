using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName = "ShootWeaponsInfo", menuName = "Setting/WeaponsInfo/Shoot")]
    public class WeaponsShootInfo : ScriptableObject
    {
        public ShootAttackInfo ShootAttackInfo;
    }

    [CreateAssetMenu(fileName = "Rifle", menuName = "Setting/WeaponsInfo/Rifle")]
    public class WeaponsRifleInfo : ScriptableObject
    {
        public float shootDamage;
        public float shootDamage_Per=1;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootCD=1;
        public float shootCD_Per=1;
        public float shootOffset;
        public float shootOffset_Per=1;
        public float shootDistance;
        public float shootDistance_Per=1;
        public float shootAimDamage;
        public float shootAimDamage_Per=1;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootAimCD=1;
        public float shootAimCD_Per=1;
        public float shootAimOffset;
        public float shootAimOffset_Per=1;
        public float shootAimDistance;
        public float shootAimDistance_Per=1;
    }
    [CreateAssetMenu(fileName = "Pistol", menuName = "Setting/WeaponsInfo/Pistol")]
    public class WeaponsPistolInfo:ScriptableObject
    {
        public float shootDamage;
        public float shootDamage_Per=1;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootCD=1;
        public float shootCD_Per=1;
        public float shootOffset;
        public float shootOffset_Per=1;
        public float shootDistance;
        public float shootDistance_Per=1;
    }
}