using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName = "CharacterInfo", menuName = "Setting/CharacterInfo")]
    public class CharacterInfo : ScriptableObject
    {
        public int health;

        public int unarmDamage;
        public float unarmAttackCheckRange;

        public float RunSpeed;
        public float WalkSpeed;
    }


}