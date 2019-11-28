using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class EquipmentManager : MonoBehaviour
    {
        public GameObject rifle1;
        public GameObject pistol1;

        public STCharacterController controller;
        public AttackMachine attackMachine;
        public Transform rifleRoot;
        public Transform pistolRoot;

        public WeaponsType myWeaponsType;
      
        [ReadOnly]
        public CharacterInfo characterInfo;

        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Weaponsbase weapons_1;
        [HideInInspector]
        public Weaponsbase weapons_2;



        int weaponsNumber;
        CharacterInfo defaultInfo;

        // Start is called before the first frame update
        void Start()
        {
            defaultInfo = Resources.Load<CharacterInfo>("DefaultCharacterInfo");
            characterInfo = ScriptableObject.CreateInstance<CharacterInfo>();
            characterInfo.CopyValue(defaultInfo);

            InitWeapons();

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


        void SetWeaponsAttack()
        {
            if(currentWeapons)
            {
                myWeaponsType = currentWeapons.weaponsType;
                currentWeapons.Open();
                SetWeaponsInfoToCharacterInfo(currentWeapons);               
               
            }else
            {
                myWeaponsType = WeaponsType.none;
                characterInfo.gunAttackInfo = new GunAttackInfo();
            }
        }
        void SetWeaponsInfoToCharacterInfo<T>(T weaponsbase) where T : Weaponsbase
        {
            if (typeof(T) == typeof(Weaponsbase))
            {
                Weaponsbase w = weaponsbase as Weaponsbase;
                GunAttackInfo gunAttackInfo = new GunAttackInfo();
                gunAttackInfo.attackCD = weaponsbase.attackCD;
                gunAttackInfo.attackDamage = weaponsbase.attackDamage;
                gunAttackInfo.attackDistance = weaponsbase.attackDistance;
                gunAttackInfo.attackOffset = weaponsbase.attackOffset;
                if (weaponsbase.hasAim)
                {
                    gunAttackInfo.hasAim = weaponsbase.hasAim;
                    gunAttackInfo.aimAttackCD = weaponsbase.aimAttackCD;
                    gunAttackInfo.aimAttackDistance = weaponsbase.aimAttackDistance;
                    gunAttackInfo.aimAttackOffset = weaponsbase.aimAttackOffset;
                }
                characterInfo.gunAttackInfo = gunAttackInfo;
            }
        }
        void SetAttackState()
        {
            attackMachine.SetWeapons(currentWeapons);
            attackMachine.gunAttackInfo = characterInfo.gunAttackInfo;
            attackMachine.unArmAttackInfo = characterInfo.unArmAttackInfo;

            controller.InitWeapons(myWeaponsType);
        }




        public void InitWeapons()
        {
            weapons_1 = GenerateWeapons(001);
            weapons_2 = GenerateWeapons(002);
            currentWeapons = weapons_1;
            weaponsNumber = 1;
            if (currentWeapons)
            {
                currentWeapons.Open();
            }
            SetWeaponsAttack();
            SetAttackState();
        }

        public void ChangeWeapons()
        {
            if (currentWeapons)
                currentWeapons.Hidden();
            if (weaponsNumber == 1)
            {
                weaponsNumber = 2;
                currentWeapons = weapons_2;
            }
            else
            {
                weaponsNumber = 1;
                currentWeapons = weapons_1;
            }
            if (currentWeapons)
            {
                currentWeapons.Open();
            }
            SetWeaponsAttack();
            SetAttackState();
        }

    }


}