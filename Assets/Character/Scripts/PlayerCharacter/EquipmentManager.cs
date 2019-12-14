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

        public TestObjectList TestObjectList;

        public STCharacterController controller;
        public AttackMachine attackMachine;
        public Transform rifleRoot;
        public Transform pistolRoot;

        [ReadOnly]
        public CharacterInfo characterInfo;
        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Weaponsbase weapons_0;
        [HideInInspector]
        public Weaponsbase weapons_1;
        // Start is called before the first frame update
        void Start()
        {
            defaultInfo = Resources.Load<CharacterInfo>("DefaultCharacterInfo");
            characterInfo = ScriptableObject.CreateInstance<CharacterInfo>();
            characterInfo.CopyValue(defaultInfo);

        }


        Weaponsbase GenerateWeapons(int id)
        {
            if (id == 001)
            {
                GameObject obj = Instantiate(TestObjectList.rifle1, rifleRoot);
                var w = obj.GetComponent<Weaponsbase>();
                w.Hidden();
                return w;
            }
            else if (id == 002)
            {
                GameObject obj = Instantiate(TestObjectList.pistol1, pistolRoot);
                var w = obj.GetComponent<Weaponsbase>();
                w.Hidden();
                return w;
            }
            else
                return null;
        }

        void SetCurrentWeapons(Weaponsbase weaponsbase)
        {
            if (currentWeapons)
            {
                currentWeapons.SubCharacterInfo(characterInfo.weaponsAttackInfo);
                currentWeapons.Hidden();
            }
            if (weaponsbase == null)
            {
                currentWeapons = null;
            }
            else
            {
                currentWeapons = weaponsbase;
                currentWeapons.AddCharacterInfo(characterInfo.weaponsAttackInfo);
            }
        }

        /// <summary>
        /// 设置武器对象
        /// </summary>
        void SetWeaponsObject()
        {
            if (currentWeapons)
            {
                currentWeapons.Open();
            }
        }
        /// <summary>
        /// 设置对应武器的环境因素
        /// </summary>
        void SetAttackMachine()
        {
            attackMachine.weaponsAttackInfo = characterInfo.weaponsAttackInfo;
            attackMachine.SetWeapons(currentWeapons);
        }

        public void InitWeapons()
        {
            weapons_0 =  GenerateWeapons(001);
            weapons_1 = GenerateWeapons(002);

            weaponsIndex = 0;
            SetCurrentWeapons(weapons_0);
            
            SetWeaponsObject();
            SetAttackMachine();
        }
        public void ChangeWeapons()
        {
            if (weaponsIndex==0)
            {
                ChangeWeapons(1);
            }
            else
            {
                ChangeWeapons(0);
            }

            SetWeaponsObject();
            SetAttackMachine();
        }
        public void ChangeWeapons(int index)
        {
            weaponsIndex = index;
            if(index==0)
            {
                SetCurrentWeapons(weapons_0);
            }else
            {
                SetCurrentWeapons(weapons_1);
            }
        }
    }


}