using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SkyTrespass.Character
{
    public class EquipmentManager : MonoBehaviour
    {
        public GameObject rifle1;
        public GameObject rifle2;

        public GameObject pistol1;
        public GameObject pistol2;
        public STCharacterController controller;
        public AttackMachine attackMachine;
        public Transform rifleRoot;
        public Transform pistolRoot;
        [HideInInspector]
        public GameObject current;


        [HideInInspector]
        public Weaponsbase currentWeapons;
        [HideInInspector]
        public Weaponsbase currentWeapons_1;
        [HideInInspector]
        public Weaponsbase currentWeapons_2;

        WeaponsType myWeaponsType;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public WeaponsType ChangeWeapons(int id)
        {
            if (current != null)
                Destroy(current);
            if (id == 000)
            {
                current = null;
                currentWeapons = null;
                attackMachine.SetWeapons(null);
                return WeaponsType.none;
            }
            else if (id == 001)
            {
                current = Instantiate(rifle1, rifleRoot);
                currentWeapons = current.GetComponent<Weaponsbase>();
                attackMachine.SetWeapons(currentWeapons);
                return currentWeapons.weaponsType;
            }
            else if (id == 002)
            {
                current = Instantiate(pistol1, pistolRoot);
                currentWeapons = current.GetComponent<Weaponsbase>();
                attackMachine.SetWeapons(currentWeapons);
                return currentWeapons.weaponsType;
            }
            return WeaponsType.none;
        }

    }
}