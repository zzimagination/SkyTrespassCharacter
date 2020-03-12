using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerUnarm : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();

        }
    }
}