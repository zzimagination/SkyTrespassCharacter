using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerIK : StateMachineBehaviour
    {
        public IKGoal goalMask;

        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            Vector3 p = animatorManager.LeftHandIK.position;
            animator.SetIKPosition(AvatarIKGoal.LeftHand, p);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        }

        [System.Flags]
        public enum IKGoal
        {
            none=0,
            every=15,
            LeftHand=1,
            LeftFoot=2,
            RightHand=4,
            RightFoot=8
        }
    }
}