using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class AttackMachine : MonoBehaviour
    {

        public Transform center;
        public float attackDistance;
        public void RaycastAttackRange()
        {
            if(Physics.BoxCast(center.position,new Vector3(0.25f,0.1f,1),Vector3.zero,Quaternion.identity,0))
            {
                Debug.Log("1");
            }
            
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(center.position, new Vector3(0.25f, 0.1f, 1));
        }
#endif
    }
}