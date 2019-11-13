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

        public GameObject current;

        public Transform rifleRoot;
        public Transform pistolRoot;

        WeaponsType myWeaponsType;


        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public void ChangeWeapons(WeaponsType weapons)
        {
            if (current != null)
                Destroy(current);
            if (weapons== WeaponsType.none)
            {
                current = null;
            }else if(weapons== WeaponsType.shoot)
            {
                current = Instantiate(rifle1, rifleRoot);
            }else if(weapons== WeaponsType.pisol)
            {
                current = Instantiate(pistol1, pistolRoot);

            }

        }

    }
}