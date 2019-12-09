using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace SkyTrespass.Character {
    public class PlayerIdle : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager.TransformUpdate();
        }
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animatorManager.RotateDelt();
            animatorManager.Idle();
        }
    }
}