using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class EquipmentManager : MonoBehaviour
    {
        int weaponsIndex;
        CharacterInfo defaultInfo;
        bool isPickWeapons;


        public TestObjectList TestObjectList;

        [ReadOnly]
        public CharacterInfo characterInfo;
        [HideInInspector]
        public bool isAim;
        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Weaponsbase weapons_0;
        [HideInInspector]
        public Weaponsbase weapons_1;
        [HideInInspector]
        public float unarmDamage;
        [HideInInspector]
        public float unarmAttackCheckRange;

        // Start is called before the first frame update
        void Start()
        {
            defaultInfo = Resources.Load<CharacterInfo>("DefaultCharacterInfo");
            characterInfo = ScriptableObject.CreateInstance<CharacterInfo>();
            characterInfo.CopyValue(defaultInfo);
            unarmDamage = defaultInfo.AttackInfo.unarmDamage;
            unarmAttackCheckRange = defaultInfo.AttackInfo.unarmAttackCheckRange;
        }


        Weaponsbase GenerateWeapons(int id)
        {
            if (id == 001)
            {
                GameObject obj = Instantiate(TestObjectList.rifle1);
                var w = obj.GetComponent<Weaponsbase>();
                w.Hidden();
                return w;
            }
            else if (id == 002)
            {
                GameObject obj = Instantiate(TestObjectList.pistol1);
                var w = obj.GetComponent<Weaponsbase>();
                w.Hidden();
                return w;
            }
            else
                return null;
        }

        void SetCurrentWeapons(int index)
        {
            weaponsIndex = index;
            if (currentWeapons)
            {
                currentWeapons.SubCharacterInfo(characterInfo.AttackInfo);
                currentWeapons.Hidden();
            }
            if (index == 0)
            {
                currentWeapons = weapons_0;
            }
            else if (index == 1)
            {
                currentWeapons = weapons_1;
            }
            if (currentWeapons)
            {
                currentWeapons.AddCharacterInfo(characterInfo.AttackInfo);
                currentWeapons.Open();
                isPickWeapons = true;
            }
        }


        public void InitWeapons()
        {
            GetWeapons(001);
            GetWeapons(002);
            weaponsIndex = 0;
        }

        public void GetWeapons(int id)
        {
            var w = GenerateWeapons(id);
            if (!weapons_0)
            {
                weapons_0 = w;
                return;
            }
            if (!weapons_1)
            {
                weapons_1 = w;
                return;
            }
            if (weaponsIndex == 0)
            {
                weapons_0.Drop();
                weapons_0 = w;
                return;
            }
            else if (weaponsIndex == 1)
            {
                weapons_1.Drop();
                weapons_1 = w;
                return;
            }


        }

        public void PickWeapons()
        {
            if (isPickWeapons)
                return;

            if (weaponsIndex == 0)
                currentWeapons = weapons_0;
            else
                currentWeapons = weapons_1;
            if (currentWeapons)
            {
                currentWeapons.AddCharacterInfo(characterInfo.AttackInfo);
                currentWeapons.Open();
                isPickWeapons = true;
            }
        }

        public void PutWeapons()
        {
            if (!isPickWeapons)
                return;
            currentWeapons.SubCharacterInfo(characterInfo.AttackInfo);
            currentWeapons.Hidden();
            //currentWeapons = null;
            isPickWeapons = false;
        }



        public void ChangeWeapons()
        {
            if (weaponsIndex == 0)
            {
                //SetCurrentWeapons(1);
                weaponsIndex = 1;
            }
            else
            {
                //SetCurrentWeapons(0);
                weaponsIndex = 0;
            }
        }

        public void ReloadBullet()
        {
            if (currentWeapons==null)
                return;
            currentWeapons.ResetAttackNumber();
        }

        public int GetBullet()
        {
            if (currentWeapons)
                return -1;
            return currentWeapons.AttackNumber;
        }

        public float GetMoveSpeed()
        {
            return isAim ? characterInfo.AimMoveSpeed : characterInfo.MoveSpeed;
        }
        public float GetAttackSpeedMul()
        {
            if (isAim)
            {
                float cd = characterInfo.AttackInfo.shootAimCD * characterInfo.AttackInfo.shootAimCD_Per;
                return cd;
            }
            else
            {
                float cd = characterInfo.AttackInfo.shootCD * characterInfo.AttackInfo.shootCD_Per;
                return cd;
            }
        }
        public float GetUnarmAttackSpeedMul()
        {
            return characterInfo.AttackInfo.unarmCD * characterInfo.AttackInfo.unarmCD_Per;
        }

        public void HiddenWeapons(bool hide)
        {
            if (currentWeapons)
            {
                if (hide)
                    currentWeapons.Hidden();
                else
                    currentWeapons.Open();
            }
        }
    }


}