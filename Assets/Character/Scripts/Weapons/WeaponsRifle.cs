using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class WeaponsRifle : Weaponsbase
    {
        public Transform leftHandIK;
        public BulletLiner bulletLiner;
        public RifleInfo rifleInfo;

        public int magazineCapacity;

        public float shootDamage;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootCD = 1;
        public float shootOffset;
        public float shootDistance;
        public float shootAimDamage;
        [Tooltip("武器动画片段的速度，1为默认速度")]
        public float shootAimCD = 1;
        public float shootAimOffset;
        public float shootAimDistance;


        [HideInInspector]
        public bool isAim;
        [HideInInspector]
        public Transform playerTransform;
        [HideInInspector]
        public Vector3 shootPoint;

        public event AttackEvent TickEvent;

        int shootNumber;
        int remainBullet;

        private void Awake()
        {
            AttackNumber = rifleInfo.magazineCapacity;
        }


        public override int DoAttackNumber()
        {
            shootNumber++;
            remainBullet = rifleInfo.magazineCapacity - shootNumber;

            if (remainBullet <= 0)
            {
                remainBullet = 0;
            }
            AttackNumber = remainBullet;
            return remainBullet;
        }

        public override int ResetAttackNumber()
        {
            if (remainBullet == rifleInfo.magazineCapacity)
                return remainBullet;
            shootNumber = 0;
            remainBullet = rifleInfo.magazineCapacity;
            AttackNumber = remainBullet;
            return remainBullet;
        }

        public override int MaxAttackNumber()
        {
            return rifleInfo.magazineCapacity;
        }

        public override void Tick()
        {
            var shootPosition = transform.localToWorldMatrix.MultiplyPoint(shootPoint);
            var shootDir = playerTransform.forward;

            float offset = isAim ? shootAimOffset : shootOffset;
            float damage = isAim ? shootAimDamage : shootDamage;
            float distance = isAim ? shootAimDistance : shootDistance;

            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, distance, (1 << 9 | 1 << 10));
            //GameObject obj = GameObject.Instantiate(bulletLinerObj);
            //obj.transform.SetParent(transform);
            //obj.transform.position = shootPosition;
            //if (isHit)
            //{
            //    obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

            //    var t = shootResult.transform.GetComponent<IDestructible>();
            //    if (t != null)
            //    {
            //        AttackInfo attackInfo = new AttackInfo();
            //        attackInfo.damage = isAimShoot ? saInfo.shootAimDamage * saInfo.shootAimDamage_Per : saInfo.shootDamage * saInfo.shootDamage_Per;
            //        t.Attack(attackInfo);
            //    }
            //}
            //else
            //{
            //    Vector3 end = shootPosition + shootDir * dis;
            //    obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            //}
            if (isHit)
            {
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

                var t = shootResult.transform.GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = damage;
                    t.Attack(attackInfo);
                }
                Debug.DrawLine(shootPosition, shootResult.point, Color.red);
            }
            else
            {
                Vector3 end = shootPosition + shootDir * distance;
                Debug.DrawLine(shootPosition, end, Color.red);
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            }

            TickEvent?.Invoke();
        }



        public override AttackCommand CreatAttackCommand()
        {
            var ac = new BulletLineAC();
            ac.bulletLiner = bulletLiner;
            ac.localPoint = shootPoint;
            ac.transform = playerTransform;
            //ac.TickEvent = TickEventInvoke;
           if(isAim)
            {
                ac.shootDamage = rifleInfo.shootAimDamage;
                ac.shootOffset = rifleInfo.shootAimOffset;
                ac.shootDistance = rifleInfo.shootAimDistance;
                return ac;

            }else
            {
                ac.shootDamage = rifleInfo.shootDamage;
                ac.shootOffset = rifleInfo.shootOffset;
                ac.shootDistance = rifleInfo.shootDistance;
                return ac;
            }
        }

        public override void AddCharacterInfo(CharacterAttackInfo finalInfo)
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

        public override void SubCharacterInfo(CharacterAttackInfo finalInfo)
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


        public Vector3 localPoint;
        public Transform transform;
        public CharacterAttackInfo info;
        public event AttackEvent TickEvent;

        public BulletLiner bulletLiner;

        Vector3 shootPosition;
        Vector3 shootDir;



        public override void Tick()
        {
            shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
            shootDir = transform.forward;
            CharacterAttackInfo saInfo = info;

            float offset = saInfo.shootOffset * saInfo.shootOffset_Per;
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
                    attackInfo.damage =  saInfo.shootDamage * saInfo.shootDamage_Per;
                    t.Attack(attackInfo);
                }
                Debug.DrawLine(shootPosition, shootResult.point,Color.red);
            }
            else
            {
                Vector3 end = shootPosition + shootDir * dis;
                Debug.DrawLine(shootPosition, end, Color.red);
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            }

            TickEvent?.Invoke();
        }
    }

    public class RifleAimAttackCommand : AttackCommand
    {
        public Vector3 localPoint;
        public Transform transform;
        public CharacterAttackInfo info;
        public AttackEvent TickEvent;

        Vector3 shootPosition;
        Vector3 shootDir;

        public override void Tick()
        {
            shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
            shootDir = transform.forward;
            CharacterAttackInfo saInfo = info;

            float offset = saInfo.shootAimOffset * saInfo.shootAimOffset_Per;
            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            float dis = saInfo.shootAimDistance * saInfo.shootAimDistance_Per ;
            bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, dis, (1 << 9 | 1 << 10));
            //GameObject obj = GameObject.Instantiate(bulletLinerObj);
            //obj.transform.SetParent(transform);
            //obj.transform.position = shootPosition;
            //if (isHit)
            //{
            //    obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

            //    var t = shootResult.transform.GetComponent<IDestructible>();
            //    if (t != null)
            //    {
            //        AttackInfo attackInfo = new AttackInfo();
            //        attackInfo.damage = isAimShoot ? saInfo.shootAimDamage * saInfo.shootAimDamage_Per : saInfo.shootDamage * saInfo.shootDamage_Per;
            //        t.Attack(attackInfo);
            //    }
            //}
            //else
            //{
            //    Vector3 end = shootPosition + shootDir * dis;
            //    obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            //}
            if (isHit)
            {
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

                var t = shootResult.transform.GetComponent<IDestructible>();
                if (t != null)
                {
                    AttackInfo attackInfo = new AttackInfo();
                    attackInfo.damage = saInfo.shootAimDamage * saInfo.shootAimDamage_Per;
                    t.Attack(attackInfo);
                }
                Debug.DrawLine(shootPosition, shootResult.point,Color.red);
            }
            else
            {
                Vector3 end = shootPosition + shootDir * dis;
                Debug.DrawLine(shootPosition, end,Color.red);
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
            }

            TickEvent?.Invoke();
        }
    }
}