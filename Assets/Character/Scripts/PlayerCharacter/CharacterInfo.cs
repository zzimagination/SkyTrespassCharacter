using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName = "CharacterInfo", menuName = "Setting/CharacterInfo")]
    public class CharacterInfo : ScriptableObject
    {

        [BoxGroup("Fist")]
        public UnArmAttackInfo unArmAttackInfo;

        public WeaponsAttackInfo weaponsAttackInfo;

        public void CopyValue(CharacterInfo info)
        {
            unArmAttackInfo = new UnArmAttackInfo();
            unArmAttackInfo.Copy(info.unArmAttackInfo);
            weaponsAttackInfo = new WeaponsAttackInfo();
            weaponsAttackInfo.Copy( info.weaponsAttackInfo);
        }
    }

    [System.Serializable]
    public class UnArmAttackInfo
    {
  
        public float damage;
        public float damage_Per;
        public float CD;
        public float CD_Per;
        public float attackCheckRange;
        public float attackCheckRange_Per;

        public void Copy(UnArmAttackInfo other)
        {
            damage = other.damage;
            damage_Per = other.damage_Per;
            CD = other.CD;
            CD_Per = other.CD_Per;
            attackCheckRange = other.attackCheckRange;
            attackCheckRange_Per = other.attackCheckRange_Per;
        }
    }

    [System.Serializable]
    public class WeaponsAttackInfo
    {
        [BoxGroup("Shoot Info")]
        public ShootAttackInfo shootAttackInfo;
        [BoxGroup("Barrage Info")]
        public BarrageAttackInfo barrageAttackInfo;

        public void Copy(WeaponsAttackInfo other)
        {
            shootAttackInfo = new ShootAttackInfo();
            shootAttackInfo.Copy(other.shootAttackInfo);
            barrageAttackInfo = new BarrageAttackInfo();
            barrageAttackInfo.Copy(other.barrageAttackInfo);
        }
    }


    [System.Serializable]
    public class ShootAttackInfo
    {
    
        public float shootDamage;
        public float shootDamage_Per;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootCD;
        public float shootCD_Per;
        public float shootOffset;
        public float shootOffset_Per;
        public float shootDistance;
        public float shootDistance_Per;
        public float shootAimDamage;
        public float shootAimDamage_Per;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootAimCD;
        public float shootAimCD_Per;
        public float shootAimOffset;
        public float shootAimOffset_Per;
        public float shootAimDistance;
        public float shootAimDistance_Per;

        public void Copy(ShootAttackInfo other)
        {
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
        }

    }
    [System.Serializable]
    public class BarrageAttackInfo
    {
        public float fireCD;

        public void Copy(BarrageAttackInfo other)
        {
            fireCD = other.fireCD;
        }
    }
}