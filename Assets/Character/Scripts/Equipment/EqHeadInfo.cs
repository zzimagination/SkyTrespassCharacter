using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    [CreateAssetMenu(fileName ="Head",menuName ="Setting/EquipmentInfo/Head")]
    public class EqHeadInfo : EquipmentInfoBases
    {
        public int defensive;
        public int health;
    }
}