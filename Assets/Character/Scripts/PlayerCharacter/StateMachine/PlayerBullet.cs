using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerBullet : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        STCharacterController characterController;
        float l;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animator.SetLayerWeight(1, 1);
            if (!characterController)
                characterController = animator.GetComponent<STCharacterController>();

            l = 0;
            animator.SetBool("isReloadBullet", true);
            var iks = animator.GetBehaviours<PlayerIK>();
            for (int i = 0; i < iks.Length; i++)
            {
                iks[i].isIk = false;
            }
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            l = stateInfo.normalizedTime;
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.ResetTrigger("bullet");
            if (l > 0.7f)
            {
                characterController.ReloadBulletComplete();
            }
            animator.SetBool("isReloadBullet", false);
            var iks = animator.GetBehaviours<PlayerIK>();
            for (int i = 0; i < iks.Length; i++)
            {
                iks[i].isIk = true;
            }
        }
    }
}