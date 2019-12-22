using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class WeaponsPistol : Weaponsbase
    {
        public PistolInfo pistolInfo;
        int shootNumber;
        int remainBullet;

        private void Awake()
        {
            AttackNumber = pistolInfo.magazineCapacity;
        }

        public override int DoAttackNumber()
        {
            shootNumber++;
            remainBullet = pistolInfo.magazineCapacity - shootNumber;

            if (remainBullet <= 0)
            {
                remainBullet = 0;
            }
            AttackNumber = remainBullet;
            return remainBullet;
        }
        public override int ResetAttackNumber()
        {
            if (remainBullet == pistolInfo.magazineCapacity)
                return remainBullet;
            shootNumber = 0;
            remainBullet = pistolInfo.magazineCapacity;
            AttackNumber = remainBullet;
            return remainBullet;
        }
        public override int MaxAttackNumber()
        {
            return pistolInfo.magazineCapacity;
        }

        public override void AddCharacterInfo(CharacterAttackInfo finalInfo)
        {
            finalInfo.magazineCapacity = pistolInfo.magazineCapacity;

            finalInfo.shootDamage += pistolInfo.shootDamage;
            finalInfo.shootDamage_Per += pistolInfo.shootDamage_Per;

            finalInfo.shootCD += pistolInfo.shootCD;
            finalInfo.shootCD_Per += pistolInfo.shootCD_Per;

            finalInfo.shootDistance += pistolInfo.shootDistance;
            finalInfo.shootDistance_Per += pistolInfo.shootDistance_Per;

            finalInfo.shootOffset += pistolInfo.shootOffset;
            finalInfo.shootOffset_Per += pistolInfo.shootOffset_Per;

           
        }

        public override void SubCharacterInfo(CharacterAttackInfo finalInfo)
        {
            finalInfo.magazineCapacity = 0;

            finalInfo.shootDamage -= pistolInfo.shootDamage;
            finalInfo.shootDamage_Per -= pistolInfo.shootDamage_Per;

            finalInfo.shootCD -= pistolInfo.shootCD;
            finalInfo.shootCD_Per -= pistolInfo.shootCD_Per;

            finalInfo.shootDistance -= pistolInfo.shootDistance;
            finalInfo.shootDistance_Per -= pistolInfo.shootDistance_Per;

            finalInfo.shootOffset -= pistolInfo.shootOffset;
            finalInfo.shootOffset_Per -= pistolInfo.shootOffset_Per;
        }
    }

    public class PistolAttackCommand : AttackCommand
    {
        public Vector3 localPoint;
        public CharacterAttackInfo info;
        public Transform transform;
        public AttackEvent TickEvent;
        public BulletLiner bulletLiner;


        Vector3 shootPosition;
        Vector3 shootDir;

        public override void Tick()
        {
            shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
            shootDir = transform.forward;
            CharacterAttackInfo saInfo = info;

            float offset =saInfo.shootOffset * saInfo.shootOffset_Per;
            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            float dis = saInfo.shootDistance * saInfo.shootDistance_Per;
            bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, dis, (1 << 9 | 1 << 10));
            //GameObject obj = GameObject.Instantiate(bulletLinerObj);
            //obj.transform.SetParent(transform);
            //obj.transform.position = shootPosition;
            if (isHit)
            {
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

                var t = shootResult.transform.GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = saInfo.shootDamage * saInfo.shootDamage_Per;
                    t.Attack(attackInfo);
                }
            }
            else
            {
                Vector3 end = shootPosition + shootDir * dis;
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            }
            TickEvent?.Invoke();
        }
    }

}