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

        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Weaponsbase weapons_1;
        [HideInInspector]
        public Weaponsbase weapons_2;

        int weaponsNumber;

        // Start is called before the first frame update
        void Start()
        {
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

        public void InitWeapons()
        {
            weapons_1 = GenerateWeapons(001);
            weapons_2 = GenerateWeapons(002);
            currentWeapons = weapons_1;
            currentWeapons.Open();
            attackMachine.SetWeapons(currentWeapons);
            weaponsNumber = 1;

            controller.InitWeapons(currentWeapons.weaponsType);
        }

        public void ChangeWeapons()
        {
            if(currentWeapons)
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
                myWeaponsType = currentWeapons.weaponsType;
                currentWeapons.Open();
            }
            else
                myWeaponsType = WeaponsType.none;
            attackMachine.SetWeapons(currentWeapons);

        }

    }
}