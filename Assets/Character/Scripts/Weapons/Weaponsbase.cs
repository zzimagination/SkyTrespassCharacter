using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class Weaponsbase : MonoBehaviour
    {
        public WeaponsType weaponsType;
        

        [ShowIf("weaponsType",WeaponsType.shoot)]
        public Transform leftIK;
        public Vector3 shootPoint;
        public GameObject bulletLinerObj;

        //public WeaponsInfo weaponsInfo;
        public float attackDamage;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float attackCD;
        public float attackDistance;
        public float attackOffset;
        public bool hasAim;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float aimAttackCD;
        public float aimAttackDistance;
        public float aimAttackOffset;

        public void Hidden()
        {
            gameObject.SetActive(false);
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public bool HasIK()
        {
            return leftIK != null;
        }
    }
}