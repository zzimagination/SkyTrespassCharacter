using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

        void SetAttackState()
        {
            attackMachine.SetWeapons(currentWeapons);
            attackMachine.gunAttackInfo = characterInfo.gunAttackInfo;
            attackMachine.unArmAttackInfo = characterInfo.unArmAttackInfo;
            //attackMachine.attackInfo.attackDamage = characterInfo.attackDamage;
            //attackMachine.attackDistance = characterInfo.attackDistance;
            //attackMachine.attackOffset = characterInfo.attackOffset;
            //attackMachine.fistAttackRange = characterInfo.fistAttackCheckRange;

            //attackMachine.aimAttackDamage = characterInfo.aimAttackDamage;
            //attackMachine.aimAttackDistance = characterInfo.aimAttackDistance;
            //attackMachine.aimAttackOffset = characterInfo.aimAttackOffset;

            controller.InitWeapons(myWeaponsType);
        }
        void SetWeaponsAttack()
        {
            if(currentWeapons)
            {
                myWeaponsType = currentWeapons.weaponsType;
                currentWeapons.Open();
                characterInfo.gunAttackInfo = currentWeapons.weaponsInfo.attackInfo;               
                //characterInfo.attackDamage = currentWeapons.weaponsInfo.attackDamage;
                //characterInfo.attackCD = currentWeapons.weaponsInfo.attackCD;
                //characterInfo.attackDistance = currentWeapons.weaponsInfo.attackDistance;
                //characterInfo.attackOffset = currentWeapons.weaponsInfo.attackOffset;

                //characterInfo.hasAim = currentWeapons.weaponsInfo.hasAim;
                //characterInfo.aimAttackDamage = currentWeapons.weaponsInfo.aimAttackDamage;
                //characterInfo.aimAttackCD = currentWeapons.weaponsInfo.aimAttackCD;
                //characterInfo.aimAttackDistance = currentWeapons.weaponsInfo.aimAttackDistance;
                //characterInfo.aimAttackOffset = currentWeapons.weaponsInfo.aimAttackOffset;
               
            }else
            {
                myWeaponsType = WeaponsType.none;
                characterInfo.gunAttackInfo = new GunAttackInfo();

                //characterInfo.attackDamage = characterInfo.fistAttackDamage;
                //characterInfo.attackCD = characterInfo.fistAttackCD;
                //characterInfo.attackDistance = 0;
                //characterInfo.attackOffset = 0;
                //characterInfo.aimAttackDamage = 0;
                //characterInfo.aimAttackCD = 0;
                //characterInfo.aimAttackDistance = 0;
                //characterInfo.aimAttackOffset = 0;
            }
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