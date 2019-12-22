using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character {
    public class PlayerFall :StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animatorManager.StopRigidbody(false);
            animatorManager.StopAttack();
            animatorManager.isFall = true;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager.TransformUpdate();
        }
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animatorManager.RotateDelt();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager.isFall = false;
        }
    }
}