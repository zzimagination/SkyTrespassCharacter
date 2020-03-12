using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    using Goods;

    public class EquipmentManager : MonoBehaviour
    {
        public class TempBackpack
        {
            List<PickUp> pickUps = new List<PickUp>();

            public int PickNumber
            {
                get
                {
                    return pickUps.Count;
                }
            }

            public PickUp GetPickUp()
            {
                if (pickUps.Count == 0)
                    return null;
                PickUp t = pickUps[0];
                pickUps.RemoveAt(0);
                t.Pick();
                return t;
            }

            public void RegisterPickUp(PickUp obj)
            {
                if (!pickUps.Contains(obj))
                    pickUps.Add(obj);

            }
            public void RemovePickUp(PickUp obj)
            {
                if (pickUps.Count == 0)
                    return;
                if (pickUps.Contains(obj))
                    pickUps.Remove(obj);
            }

        }


        const int WeaponsSpace = 2;

        WeaponsFist fist;

        public TestObjectList TestObjectList;
        public BulletLinerPool BulletLinerPool;
        public Transform rightHand;
        public int bulletsNumber;

        [HideInInspector]
        public bool isAim;
        [HideInInspector]
        public bool unArm;
        [HideInInspector]
        public Weaponsbase currentWeapons;
        //[HideInInspector]
        //public Weaponsbase weapons_0;
        //[HideInInspector]
        //public Weaponsbase weapons_1;
        [HideInInspector]
        public Weaponsbase[] weaponsArray = new Weaponsbase[WeaponsSpace];

        //[HideInInspector]
        //public int unarmDamage;
        //[HideInInspector]
        //public float unarmAttackCheckRange;
        [HideInInspector]
        public float RunSpeed;
        [HideInInspector]
        public float WalkSpeed;

        public TempBackpack tempBackpack;
        public ParseInformation lastPickup;


        int weaponsIndex;
        public int WeaponsIndex
        {
            get
            {
                return weaponsIndex;
            }
            set
            {
                if (value >= WeaponsSpace)
                {
                    weaponsIndex = 0;
                }
                else if (value < 0)
                    weaponsIndex = WeaponsSpace - 1;
                else
                    weaponsIndex = value;
            }
        }


        int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value < 0)
                    health = 0;
                else if (value > 100)
                    health = 100;
                else
                    health = value;
            }
        }


        int armor;
        public int Armor
        {
            get
            {
                return armor;
            }
            set
            {
                if (value < 0)
                    armor = 0;
                else
                    armor = value;
            }
        }
        public float AvoidInjury
        {
            get
            {
                float avoid = Mathf.Log10(Armor / 10.0f) + 1;
                return avoid/3;
            }

        }


        public CharacterInfo defaultInfo;
        private void Awake()
        {
            tempBackpack = new TempBackpack();
            if (defaultInfo)
                defaultInfo = Resources.Load<CharacterInfo>("DefaultCharacterInfo");
        }


        Weaponsbase GenerateWeapons(int id)
        {
            if (id == 001)
            {
                GameObject obj = Instantiate(TestObjectList.rifle1, rightHand);
                var w = obj.GetComponent<Weaponsbase>();
                w.linerPool = BulletLinerPool;
                w.playerTransform = transform;
                w.Close();
                return w;
            }
            else if (id == 002)
            {
                GameObject obj = Instantiate(TestObjectList.pistol1, rightHand);
                var w = obj.GetComponent<Weaponsbase>();
                w.linerPool = BulletLinerPool;
                w.playerTransform = transform;
                w.Close();
                return w;
            }
            else
                return null;
        }


        void InitWeapons()
        {
            for (int i = 0; i < weaponsArray.Length; i++)
            {
                weaponsArray[i] = fist;
            }
            GetWeapons(001);
            // GetWeapons(002);
            WeaponsIndex = 0;

        }


        public void InitEquipment()
        {
            fist = GetComponent<WeaponsFist>();
            fist.unarmDamage = defaultInfo.unarmDamage;
            fist.unarmAttackCheckRange = defaultInfo.unarmAttackCheckRange;

            RunSpeed = defaultInfo.RunSpeed;
            WalkSpeed = defaultInfo.WalkSpeed;
            health = defaultInfo.health;
            bulletsNumber = 100;

            InitWeapons();
        }
        public void GetWeapons(int id)
        {
            var w = GenerateWeapons(id);

            for (int i = 0; i < weaponsArray.Length; i++)
            {
                if (!weaponsArray[i])
                {
                    weaponsArray[i] = w;
                    return;
                }
            }
            weaponsArray[WeaponsIndex].Drop();
            weaponsArray[WeaponsIndex] = w;
        }

        public void GetWeapons(GameObject obj)
        {
            var w = obj.GetComponent<Weaponsbase>();
            if (!w)
                return;
            w.linerPool = BulletLinerPool;
            w.playerTransform = transform;
            w.Close();

            for (int i = 0; i < weaponsArray.Length; i++)
            {
                if (!weaponsArray[i])
                {
                    weaponsArray[i] = w;
                    return;
                }
            }
            weaponsArray[WeaponsIndex].Drop();
            weaponsArray[WeaponsIndex] = w;
        }

        public void UpWeapons(int index)
        {
            WeaponsIndex = index;
            currentWeapons = weaponsArray[WeaponsIndex];
        }

        public WeaponsType GetCurrentWeaponsType()
        {
           return currentWeapons.weaponsType;
        }

        public void PickWeapons(int index)
        {
            WeaponsIndex = index;
            weaponsArray[WeaponsIndex].Open();
        }

        public void PutWeapons(int index)
        {
            WeaponsIndex = index;
            weaponsArray[WeaponsIndex].Close();
        }

        public void ReloadBulletComplete()
        {
            currentWeapons.RemainBullet = currentWeapons.magazineCapacity;
        }

        public float GetMoveSpeed()
        {
            switch (currentWeapons.weaponsType)
            {
                case WeaponsType.none:
                    return RunSpeed;
                case WeaponsType.rifle:
                    if (isAim)
                        return WalkSpeed;
                    else
                        return RunSpeed;
                case WeaponsType.pistol:
                default:
                    return RunSpeed;
            }
        }


        //public void ChangeAim(bool isAim)
        //{

        //    if (!unArm)
        //    {
        //        this.isAim = isAim;
        //        currentWeapons.ChangeAim(isAim);
        //    }
        //    else
        //    {
        //        isAim = false;
        //    }
        //}

        //public bool WeaponsCanAttack()
        //{
        //    if (currentWeapons)
        //    {
        //        var r = currentWeapons.RemainBullet != 0;
        //        return r;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool CanPick()
        //{
        //    return tempBackpack.PickNumber > 0;
        //}

        public void PickObject()
        {
            var p = tempBackpack.GetPickUp();
            var info = p.Pick();

            var parseInfo = ParseGoods.ParseID(info.id);
            if (parseInfo.pickType == PickType.weapons)
            {
                //GameObject mid = Resources.Load<GameObject>(parseInfo.objPath);
                //GameObject obj = Instantiate(mid, rightHand);
                //GetWeapons(obj);
            }
            else if (parseInfo.pickType == PickType.health)
            {
                Health += 10;
            }
            lastPickup = parseInfo;
        }
        





    }



}