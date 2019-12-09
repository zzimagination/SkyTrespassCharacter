using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class WeaponsPistol : Weaponsbase
    {
        public Vector3 shootLocalPoint;
        public GameObject bulletLinerObj;
        public WeaponsPistolInfo info;

        private void OnEnable()
        {
            var command = new PistolAttackCommand();
            command.bulletLinerObj = bulletLinerObj;
            attackCommand = command;
        }

        public override void AddCharacterInfo(WeaponsAttackInfo finalInfo)
        {
            finalInfo.shootAttackInfo.shootDamage += info.shootDamage;
            finalInfo.shootAttackInfo.shootDamage_Per += info.shootDamage_Per;

            finalInfo.shootAttackInfo.shootCD += info.shootCD;
            finalInfo.shootAttackInfo.shootCD_Per += info.shootCD_Per;

            finalInfo.shootAttackInfo.shootDistance += info.shootDistance;
            finalInfo.shootAttackInfo.shootDistance_Per += info.shootDistance_Per;

            finalInfo.shootAttackInfo.shootOffset += info.shootOffset;
            finalInfo.shootAttackInfo.shootOffset_Per += info.shootOffset_Per;

           
        }

        public override void SubCharacterInfo(WeaponsAttackInfo finalInfo)
        {
            finalInfo.shootAttackInfo.shootDamage -= info.shootDamage;
            finalInfo.shootAttackInfo.shootDamage_Per -= info.shootDamage_Per;

            finalInfo.shootAttackInfo.shootCD -= info.shootCD;
            finalInfo.shootAttackInfo.shootCD_Per -= info.shootCD_Per;

            finalInfo.shootAttackInfo.shootDistance -= info.shootDistance;
            finalInfo.shootAttackInfo.shootDistance_Per -= info.shootDistance_Per;

            finalInfo.shootAttackInfo.shootOffset -= info.shootOffset;
            finalInfo.shootAttackInfo.shootOffset_Per -= info.shootOffset_Per;
        }
    }

    public class PistolAttackCommand : AttackCommand
    {

        public GameObject bulletLinerObj;

        Transform shootPoint;
        Vector3 shootPosition;
        Vector3 shootDir;
        WeaponsAttackInfo characterInfo;
        bool isAimShoot;
        Transform transform;
        public override void Prepare(AttackMachine attackMachine)
        {
            shootPoint = attackMachine.shootPoint;

            isAimShoot = attackMachine.isAim;
            characterInfo = attackMachine.weaponsAttackInfo;
            transform = attackMachine.transform;

        }
        public override void Start()
        {

        }


        public override void Keep()
        {
            shootPosition = shootPoint.position;
            shootDir = shootPoint.forward;
            ShootAttackInfo saInfo = characterInfo.shootAttackInfo;

            float offset = isAimShoot ? (saInfo.shootAimOffset * saInfo.shootAimOffset_Per) : saInfo.shootOffset * saInfo.shootOffset_Per;
            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            float dis = isAimShoot ? saInfo.shootAimDistance * saInfo.shootAimDistance_Per : saInfo.shootDistance * saInfo.shootDistance_Per;
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
                    attackInfo.damage = isAimShoot ? saInfo.shootAimDamage * saInfo.shootAimDamage_Per : saInfo.shootDamage * saInfo.shootDamage_Per;
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