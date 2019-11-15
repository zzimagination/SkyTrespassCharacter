using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace SkyTrespass.Character
{
    public class PlayerUnarmAttack : StateMachineBehaviour
    {
        int i;
        float attackTimer;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            i = 0;
            animator.SetLayerWeight(1, 1);
            attackTimer = 0;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(stateInfo.normalizedTime-i>0.9)
            {
                i++;
                Random.InitState(i);
                int t= Random.Range(0, 3);
                animator.SetFloat("unarmAttackType", 0.5f * t);

                
            }

            if(stateInfo.normalizedTime-attackTimer>0.5f)
            {
                attackTimer++;
                animator.GetComponent<AttackMachine>().RaycastAttackRange();
            }


        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            animator.SetLayerWeight(1, 0);
        }
    }
}