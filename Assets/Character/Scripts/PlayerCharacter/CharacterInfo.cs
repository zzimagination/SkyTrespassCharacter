using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName = "CharacterInfo", menuName = "Setting/CharacterInfo")]
    public class CharacterInfo : ScriptableObject
    {
        public CharacterAttackInfo AttackInfo;

        public float MoveSpeed;
        public float AimMoveSpeed;

        public void CopyValue(CharacterInfo info)
        {
            MoveSpeed = info.MoveSpeed;
            AimMoveSpeed = info.AimMoveSpeed;
            AttackInfo = new CharacterAttackInfo();
            AttackInfo.Copy( info.AttackInfo);
        }
    }

    //[System.Serializable]
    //public class UnArmAttackInfo
    //{
  
    //    public float damage;
    //    public float damage_Per;
    //    public float CD;
    //    public float CD_Per;
    //    public float attackCheckRange;
    //    public float attackCheckRange_Per;

    //    public void Copy(UnArmAttackInfo other)
    //    {
    //        damage = other.damage;
    //        damage_Per = other.damage_Per;
    //        CD = other.CD;
    //        CD_Per = other.CD_Per;
    //        attackCheckRange = other.attackCheckRange;
    //        attackCheckRange_Per = other.attackCheckRange_Per;
    //    }
    //}

    [System.Serializable]
    public class CharacterAttackInfo
    {
        [BoxGroup("Unarm")]
        public float unarmDamage;
        [BoxGroup("Unarm")]
        public float unarmDamage_Per;
        [BoxGroup("Unarm")]
        public float unarmCD;
        [BoxGroup("Unarm")]
        public float unarmCD_Per;
        [BoxGroup("Unarm")]
        public float unarmAttackCheckRange;
        [BoxGroup("Unarm")]
        public float unarmAttackCheckRange_Per;

        [BoxGroup("Shoot Info")]
        public int magazineCapacity;
        [BoxGroup("Shoot Info")]
        public float shootDamage;
        [BoxGroup("Shoot Info")]
        public float shootDamage_Per;
        [BoxGroup("Shoot Info")]
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootCD;
        [BoxGroup("Shoot Info")]
        public float shootCD_Per;
        [BoxGroup("Shoot Info")]
        public float shootOffset;
        [BoxGroup("Shoot Info")]
        public float shootOffset_Per;
        [BoxGroup("Shoot Info")]
        public float shootDistance;
        [BoxGroup("Shoot Info")]
        public float shootDistance_Per;
        [BoxGroup("Shoot Info")]
        public float shootAimDamage;
        [BoxGroup("Shoot Info")]
        public float shootAimDamage_Per;
        [BoxGroup("Shoot Info")]
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootAimCD;
        [BoxGroup("Shoot Info")]
        public float shootAimCD_Per;
        [BoxGroup("Shoot Info")]
        public float shootAimOffset;
        [BoxGroup("Shoot Info")]
        public float shootAimOffset_Per;
        [BoxGroup("Shoot Info")]
        public float shootAimDistance;
        [BoxGroup("Shoot Info")]
        public float shootAimDistance_Per;

        [BoxGroup("Barrage Info")]
        public float fireCD;

        public void Copy(CharacterAttackInfo other)
        {

            unarmDamage = other.unarmDamage;
            unarmDamage_Per = other.unarmDamage_Per;
            unarmCD = other.unarmCD;
            unarmCD_Per = other.unarmCD_Per;
            unarmAttackCheckRange = other.unarmAttackCheckRange;
            unarmAttackCheckRange_Per = other.unarmAttackCheckRange_Per;

            shootDamage = other.shootDamage;
            shootDamage_Per = other.shootDamage_Per;
            shootCD = other.shootCD;
            shootCD_Per = other.shootCD_Per;
            shootOffset = other.shootOffset;
            shootOffset_Per = other.shootOffset_Per;
            shootDistance = other.shootDistance;
            shootDistance_Per = other.shootDistance_Per;
            shootAimDamage = other.shootAimDamage;
            shootAimDamage_Per = other.shootAimDamage_Per;
            shootAimCD = other.shootAimCD;
            shootAimCD_Per = other.shootAimCD_Per;
            shootAimOffset = other.shootAimOffset;
            shootAimOffset_Per = other.shootAimOffset_Per;
            shootAimDistance = other.shootAimDistance;
            shootAimDistance_Per = other.shootAimDistance_Per;

            fireCD = other.fireCD;
        }
    }

}