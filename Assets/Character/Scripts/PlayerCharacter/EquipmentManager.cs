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


        public GameObject rifle1;
        public GameObject pistol1;

        public STCharacterController controller;
        public AttackMachine attackMachine;
        public Transform rifleRoot;
        public Transform pistolRoot;

        [ReadOnly]
        public CharacterInfo characterInfo;
        [HideInInspector]
        public WeaponsType myWeaponsType;
        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Weaponsbase weapons_0;
        [HideInInspector]
        public Weaponsbase weapons_1;


        public int WeaponsIndex
        {
            get { return weaponsIndex; }
            private set
            {
                if (value > 1)
                {
                    weaponsIndex = 0;
                }
                else if (value < 0)
                {
                    weaponsIndex = 1;
                }
                else
                {
                    weaponsIndex = value;
                }
            }
        }


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
                GameObject obj = Instantiate(rifle1, rifleRoot);
                var w = obj.GetComponent<Weaponsbase>();
                w.Hidden();
                return w;
            }
            else if (id == 002)
            {
                GameObject obj = Instantiate(pistol1, pistolRoot);
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
                myWeaponsType = WeaponsType.none;
            }
            else
            {
                myWeaponsType = weaponsbase.weaponsType;
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
            attackMachine.unArmAttackInfo = characterInfo.unArmAttackInfo;
            attackMachine.SetWeapons(currentWeapons);
        }

        public void InitWeapons()
        {
            weapons_0 =  GenerateWeapons(001);
            weapons_1 = GenerateWeapons(002);

            SetCurrentWeapons(weapons_0);
            WeaponsIndex = 0;

            SetWeaponsObject();
            SetAttackMachine();
        }

        public void ChangeWeapons()
        {
            if (WeaponsIndex == 0)
            {
                WeaponsIndex = 1;
                SetCurrentWeapons(weapons_1);
            }
            else
            {
                WeaponsIndex = 0;
                SetCurrentWeapons(weapons_0);
            }

            SetWeaponsObject();
            SetAttackMachine();
        }

    }


}