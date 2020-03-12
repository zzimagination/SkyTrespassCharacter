using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerDeath : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        STCharacterController controller;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if(animatorManager)
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();

        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {

        }
    }
}