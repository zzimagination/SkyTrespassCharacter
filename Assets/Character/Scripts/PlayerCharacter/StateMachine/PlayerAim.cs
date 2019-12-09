using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerAim : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animatorManager.physics_MoveSpeed = animatorManager.aimMoveSpeed;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animatorManager.physics_MoveSpeed = animatorManager.moveSpeed;
        }
    }
}