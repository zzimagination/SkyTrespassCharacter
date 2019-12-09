using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerUpBodyReady : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animatorManager.keepAttack = false;
            animator.SetLayerWeight(1, 0);
        }

    }
}