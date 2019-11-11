using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace SkyTrespass.Character
{
    public class PlayerUnarmAttack : StateMachineBehaviour
    {
        int i;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            i = 0;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(stateInfo.normalizedTime-i>0.9)
            {
                i++;
                Random.InitState((int)stateInfo.normalizedTime);
                int t= Random.Range(0, 3);
                animator.SetFloat("unarmAttackType", 0.5f * t);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            
        }
    }
}