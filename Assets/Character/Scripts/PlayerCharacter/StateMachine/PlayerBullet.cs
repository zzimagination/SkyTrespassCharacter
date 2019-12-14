using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerBullet : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;   
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animatorManager.EnterReloadInvoke();
            animator.SetLayerWeight(1, 1);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager.ExitReloadInvoke();
            animator.ResetTrigger("bullet");
        }
    }
}