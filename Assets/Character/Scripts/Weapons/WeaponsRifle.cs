using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class WeaponsRifle : Weaponsbase
    {

        public Vector3 shootLocalPoint;
        public Transform leftHandIK;
        public GameObject bulletLinerObj;
        public RifleInfo rifleInfo;

        private void OnEnable()
        {
            var command = new RifleAttackCommand();
            command.bulletLinerObj = bulletLinerObj;
            command.localPoint = shootLocalPoint;
            attackCommand = command;
        }

        public override void AddCharacterInfo(WeaponsAttackInfo finalInfo)
        {

            finalInfo.magazineCapacity = rifleInfo.magazineCapacity;

            finalInfo.shootDamage += rifleInfo.shootDamage;
            finalInfo.shootDamage_Per += rifleInfo.shootDamage_Per;

            finalInfo.shootCD += rifleInfo.shootCD;
            finalInfo.shootCD_Per += rifleInfo.shootCD_Per;

            finalInfo.shootDistance += rifleInfo.shootDistance;
            finalInfo.shootDistance_Per += rifleInfo.shootDistance_Per;

            finalInfo.shootOffset += rifleInfo.shootOffset;
            finalInfo.shootOffset_Per += rifleInfo.shootOffset_Per;

            finalInfo.shootAimDamage += rifleInfo.shootAimDamage;
            finalInfo.shootAimDamage_Per += rifleInfo.shootAimDamage_Per;

            finalInfo.shootAimCD += rifleInfo.shootAimCD;
            finalInfo.shootAimCD_Per += rifleInfo.shootAimCD_Per;

            finalInfo.shootAimDistance += rifleInfo.shootAimDistance;
            finalInfo.shootAimDistance_Per += rifleInfo.shootAimDistance_Per;

            finalInfo.shootAimOffset += rifleInfo.shootAimOffset;
            finalInfo.shootAimOffset_Per += rifleInfo.shootAimOffset_Per;
        }

        public override void SubCharacterInfo(WeaponsAttackInfo finalInfo)
        {
            finalInfo.magazineCapacity = 0;

            finalInfo.shootDamage -= rifleInfo.shootDamage;
            finalInfo.shootDamage_Per -= rifleInfo.shootDamage_Per;

            finalInfo.shootCD -= rifleInfo.shootCD;
            finalInfo.shootCD_Per -= rifleInfo.shootCD_Per;

            finalInfo.shootDistance -= rifleInfo.shootDistance;
            finalInfo.shootDistance_Per -= rifleInfo.shootDistance_Per;

            finalInfo.shootOffset -= rifleInfo.shootOffset;
            finalInfo.shootOffset_Per -= rifleInfo.shootOffset_Per;

            finalInfo.shootAimCD -= rifleInfo.shootAimCD;
            finalInfo.shootAimCD_Per -= rifleInfo.shootAimCD_Per;

            finalInfo.shootAimDamage -= rifleInfo.shootAimDamage;
            finalInfo.shootAimDamage_Per -= rifleInfo.shootAimDamage_Per;

            finalInfo.shootAimDistance -= rifleInfo.shootAimDistance;
            finalInfo.shootAimDistance_Per -= rifleInfo.shootAimDistance_Per;

            finalInfo.shootAimOffset -= rifleInfo.shootAimOffset;
            finalInfo.shootAimOffset_Per -= rifleInfo.shootAimOffset_Per;
        }
    }


    public class RifleAttackCommand : AttackCommand
    {
        public GameObject bulletLinerObj;
        public Vector3 localPoint;
        Vector3 shootPosition;
        Vector3 shootDir;
        WeaponsAttackInfo info;
        bool isAimShoot;
        Transform transform;

        public override void Prepare(AttackMachine attackMachine)
        {
            isAimShoot = attackMachine.isAim;
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

            float offset = isAimShoot ? (saInfo.shootAimOffset * saInfo.shootAimOffset_Per) : saInfo.shootOffset * saInfo.shootOffset_Per;
            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            float dis = isAimShoot ? saInfo.shootAimDistance * saInfo.shootAimDistance_Per : saInfo.shootDistance * saInfo.shootDistance_Per;
            bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, dis, (1 << 9 | 1 << 10));
            GameObject obj =GameObject.Instantiate(bulletLinerObj);
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