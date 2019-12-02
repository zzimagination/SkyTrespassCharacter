using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character {
    public class PlayerFall :StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        STCharacterController controller;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            controller = animator.GetComponent<STCharacterController>();
            controller.StopRigidbody(false);

            controller.MainButtonPress = null;
            controller.MainButtonUp = null;
            controller.AimButton = null;
            controller.isFall = true;

            controller.StopAttack();


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
            controller.MainButtonPress = controller.PickOrAttack;
            controller.AimButton = controller.AutoChangeAim;
            controller.MainButtonUp = controller.StopAttack;
            controller.isFall = false;
        }
    }
}