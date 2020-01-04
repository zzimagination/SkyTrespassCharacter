using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class WeaponsRifle : Weaponsbase
    {
 
        public float shootDamage;
        public float shootOffset;
        public float shootDistance;
        public float shootAimOffset;

        [ReadOnly]
        public bool isAim;

        [HideInInspector]
        public Transform attackTransform;
        [HideInInspector]
        public Vector3 shootPoint;

        private void Awake()
        {
            RemainBullet = magazineCapacity;
        }


        public override void InitWeapons()
        {
            RemainBullet = magazineCapacity;
        }

        public override int ReloadBullet(int standby)
        {
            int n = magazineCapacity - RemainBullet;
            if (n < 0)
                throw new System.Exception("换弹出错");
            if (standby >= n)
            {
                RemainBullet = magazineCapacity;
                return standby - n;
            }
            else
            {
                RemainBullet = RemainBullet + standby;
                return 0;
            }
        }

        public override void ChangeAim(bool aim)
        {
            this.isAim = aim;
        }

        public override void AttackPrepare(AttackMachine attackMachine)
        {
            this.attackMachine = attackMachine;
            if (RemainBullet <= 0)
            {
                attackMachine.StopAttack();
            }
            attackTransform = attackMachine.transform;
            shootPoint = attackMachine.shootPoint;
        }

        public override void AttackTick()
        {
            var shootPosition = attackTransform.position + shootPoint;
            var shootDir = attackTransform.forward;

            float damage = shootDamage;
            float distance = shootDistance;
            float offset = isAim ? shootAimOffset : shootOffset;

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
                linerPool.CreatLiner(shootPosition, shootResult.point);
            }
            else
            {
                Vector3 end = shootPosition + shootDir * distance;
                //Debug.DrawLine(shootPosition, end, Color.red);
                //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
                linerPool.CreatLiner(shootPosition, end);
            }
            RemainBullet--;
            if(RemainBullet<=0)
            {
                attackMachine.StopAttack();
            }
        }

    }


    //public class RifleAttackCommand : AttackCommand
    //{


    //    public Vector3 localPoint;
    //    public Transform transform;
    //    public CharacterAttackInfo info;
    //    public event AttackEvent TickEvent;

    //    public BulletLiner bulletLiner;

    //    Vector3 shootPosition;
    //    Vector3 shootDir;



    //    public override void Tick()
    //    {
    //        shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
    //        shootDir = transform.forward;
    //        CharacterAttackInfo saInfo = info;

    //        float offset = saInfo.shootOffset * saInfo.shootOffset_Per;
    //        Random.InitState(RandomSeed.GetSeed());
    //        float angle = Random.Range(-offset, offset);
    //        var qa = Quaternion.AngleAxis(angle, Vector3.up);
    //        shootDir = qa * shootDir;

    //        float dis = saInfo.shootDistance * saInfo.shootDistance_Per;
    //        bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, dis, (1 << 9 | 1 << 10));
    //        //GameObject obj = GameObject.Instantiate(bulletLinerObj);
    //        //obj.transform.SetParent(transform);
    //        //obj.transform.position = shootPosition;
    //        if (isHit)
    //        {
    //            //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

    //            var t = shootResult.transform.GetComponent<IDestructible>();
    //            if (t != null)
    //            {
    //                AttackInfo attackInfo = new AttackInfo();
    //                attackInfo.damage = saInfo.shootDamage * saInfo.shootDamage_Per;
    //                t.Attack(attackInfo);
    //            }
    //            Debug.DrawLine(shootPosition, shootResult.point, Color.red);
    //        }
    //        else
    //        {
    //            Vector3 end = shootPosition + shootDir * dis;
    //            Debug.DrawLine(shootPosition, end, Color.red);
    //            //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
    //        }

    //        TickEvent?.Invoke();
    //    }
    //}

    //public class RifleAimAttackCommand : AttackCommand
    //{
    //    public Vector3 localPoint;
    //    public Transform transform;
    //    public CharacterAttackInfo info;
    //    public AttackEvent TickEvent;

    //    Vector3 shootPosition;
    //    Vector3 shootDir;

    //    public override void Tick()
    //    {
    //        shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
    //        shootDir = transform.forward;
    //        CharacterAttackInfo saInfo = info;

    //        float offset = saInfo.shootAimOffset * saInfo.shootAimOffset_Per;
    //        Random.InitState(RandomSeed.GetSeed());
    //        float angle = Random.Range(-offset, offset);
    //        var qa = Quaternion.AngleAxis(angle, Vector3.up);
    //        shootDir = qa * shootDir;

    //        float dis = saInfo.shootAimDistance * saInfo.shootAimDistance_Per;
    //        bool isHit = Physics.Raycast(shootPosition, shootDir, out RaycastHit shootResult, dis, (1 << 9 | 1 << 10));
    //        //GameObject obj = GameObject.Instantiate(bulletLinerObj);
    //        //obj.transform.SetParent(transform);
    //        //obj.transform.position = shootPosition;
    //        //if (isHit)
    //        //{
    //        //    obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

    //        //    var t = shootResult.transform.GetComponent<IDestructible>();
    //        //    if (t != null)
    //        //    {
    //        //        AttackInfo attackInfo = new AttackInfo();
    //        //        attackInfo.damage = isAimShoot ? saInfo.shootAimDamage * saInfo.shootAimDamage_Per : saInfo.shootDamage * saInfo.shootDamage_Per;
    //        //        t.Attack(attackInfo);
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    Vector3 end = shootPosition + shootDir * dis;
    //        //    obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
    //        //}
    //        if (isHit)
    //        {
    //            //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, shootResult.point);

    //            var t = shootResult.transform.GetComponent<IDestructible>();
    //            if (t != null)
    //            {
    //                AttackInfo attackInfo = new AttackInfo();
    //                attackInfo.damage = saInfo.shootAimDamage * saInfo.shootAimDamage_Per;
    //                t.Attack(attackInfo);
    //            }
    //            Debug.DrawLine(shootPosition, shootResult.point, Color.red);
    //        }
    //        else
    //        {
    //            Vector3 end = shootPosition + shootDir * dis;
    //            Debug.DrawLine(shootPosition, end, Color.red);
    //            //obj.GetComponent<BulletLiner>().SetPoint(shootPosition, end);
    //        }

    //        TickEvent?.Invoke();
    //    }
    //}
}