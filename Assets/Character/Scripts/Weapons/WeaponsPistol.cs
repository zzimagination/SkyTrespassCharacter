using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class WeaponsPistol : Weaponsbase
    {
        public Vector3 shootLocalPoint;
        public GameObject bulletLinerObj;
        public PistolInfo pistolInfo;

        private void OnEnable()
        {
            var command = new PistolAttackCommand();
            command.bulletLinerObj = bulletLinerObj;
            command.localPoint = shootLocalPoint;
            attackCommand = command;
        }

        public override void AddCharacterInfo(WeaponsAttackInfo finalInfo)
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

        public override void SubCharacterInfo(WeaponsAttackInfo finalInfo)
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

        public GameObject bulletLinerObj;
        public Vector3 localPoint;
        Vector3 shootPosition;
        Vector3 shootDir;
        WeaponsAttackInfo info;
        Transform transform;

        public override void Prepare(AttackMachine attackMachine)
        {
            info = attackMachine.weaponsAttackInfo;
            transform = attackMachine.transform;
        }
        public override void Start()
        {
            
            
        }


        public override void Keep()
        {
            shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
            shootDir = transform.forward;
            WeaponsAttackInfo saInfo = info;

            float offset =saInfo.shootOffset * saInfo.shootOffset_Per;
            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            float dis = saInfo.shootDistance * saInfo.shootDistance_Per;
            bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, dis, (1 << 9 | 1 << 10));
            GameObject obj = GameObject.Instantiate(bulletLinerObj);
            obj.transform.SetParent(transform);
            obj.transform.position = shootPosition;
            if (isHit)
            {
                obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

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
                obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            }
        }

        public override void End()
        {
        }


    }

}