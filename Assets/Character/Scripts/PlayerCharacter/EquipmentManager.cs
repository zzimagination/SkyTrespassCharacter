using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class EquipmentManager : MonoBehaviour
    {
        public TestObjectList TestObjectList;
        public BulletLinerPool BulletLinerPool;
        public Transform rightHand;
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

        [HideInInspector]
        public float RunSpeed;
        [HideInInspector]
        public float WalkSpeed;
   
        public int bulletsNumber;


        int weaponsIndex;
        float moveSpeed;
        bool isPickWeapons;

        CharacterInfo defaultInfo;
        private void Awake()
        {

        }


        Weaponsbase GenerateWeapons(int id)
        {
            if (id == 001)
            {
                GameObject obj = Instantiate(TestObjectList.rifle1,rightHand);
                var w = obj.GetComponent<Weaponsbase>();
                w.linerPool = BulletLinerPool;
                w.Close();
                return w;
            }
            else if (id == 002)
            {
                GameObject obj = Instantiate(TestObjectList.pistol1,rightHand);
                var w = obj.GetComponent<Weaponsbase>();
                w.linerPool = BulletLinerPool;
                w.Close();
                return w;
            }
            else
                return null;
        }

        public void InitEquipment()
        {
            defaultInfo = Resources.Load<CharacterInfo>("DefaultCharacterInfo");
            unarmDamage = defaultInfo.AttackInfo.unarmDamage;
            unarmAttackCheckRange = defaultInfo.AttackInfo.unarmAttackCheckRange;
            RunSpeed = defaultInfo.RunSpeed;
            WalkSpeed = defaultInfo.WalkSpeed;
            moveSpeed = RunSpeed;
            InitWeapons();
        }


        public void InitWeapons()
        {
            GetWeapons(001);
            //GetWeapons(002);
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
                currentWeapons.Open();
                isPickWeapons = true;
            }
        }

        public void PutWeapons()
        {
            if (!isPickWeapons)
                return;
            currentWeapons.Close();
            currentWeapons = null;
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

        public bool CanReloadBullet()
        {
            if (bulletsNumber == 0)
                return false;

            if (weaponsIndex == 0)
                currentWeapons = weapons_0;
            else if (weaponsIndex == 1)
                currentWeapons = weapons_1;
            if (currentWeapons == null)
            {
                return false;
            }
            if (currentWeapons.RemainBullet == currentWeapons.magazineCapacity)
                return false;
            return true;
        }
        public void ReloadBullet()
        {
            bulletsNumber = currentWeapons.ReloadBullet(bulletsNumber);
        }


        public float GetMoveSpeed()
        {
            return moveSpeed;
        }


        public void ChangeAim(bool isAim)
        {
            this.isAim = isAim;
            if(currentWeapons)
            {
                currentWeapons.ChangeAim(isAim);
            }
            moveSpeed = isAim ? WalkSpeed : RunSpeed;
        }

        public bool WeaponsCanAttack()
        {
            if(currentWeapons)
            {
                var r=currentWeapons.RemainBullet != 0;
                return r;
            }else
            {
                return false;
            }
        }
    }


}