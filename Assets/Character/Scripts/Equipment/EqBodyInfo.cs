using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName ="Body",menuName ="Setting/EquipmentInfo/Body")]
    public class EqBodyInfo : EquipmentInfoBases
    {
        public int health;
        public int defensive;
    }
}