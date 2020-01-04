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

        public float RunSpeed;
        public float WalkSpeed;
    }

    [System.Serializable]
    public class CharacterAttackInfo
    {
        [BoxGroup("Unarm")]
        public float unarmDamage;
        [BoxGroup("Unarm")]
        public float unarmAttackCheckRange;

    }

}