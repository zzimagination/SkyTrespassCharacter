using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class BulletLineAC : AttackCommand
    {
        public Vector3 localPoint;
        public Transform transform;
        public AttackEvent TickEvent;
        public BulletLiner bulletLiner;
        public float shootOffset;
        public float shootDistance;
        public float shootDamage;

        Vector3 shootPosition;
        Vector3 shootDir;

        public override void Tick()
        {
            shootPosition = transform.localToWorldMatrix.MultiplyPoint(localPoint);
            shootDir = transform.forward;

            float offset = shootOffset;
            Random.InitState(RandomSeed.GetSeed());
            float angle = Random.Range(-offset, offset);
            var qa = Quaternion.AngleAxis(angle, Vector3.up);
            shootDir = qa * shootDir;

            float dis = shootDistance;
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
                    attackInfo.damage = shootDamage;
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