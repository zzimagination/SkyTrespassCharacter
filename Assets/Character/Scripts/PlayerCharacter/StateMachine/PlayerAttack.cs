using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerAttack : StateMachineBehaviour
    {
        public float attackNormalizedTime;
        protected AttackStage stage;
        protected float attackTimer;
        protected PlayerAnimatorManager animatorManager;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            attackTimer = 0;
            animator.SetLayerWeight(1, 1);
            stage = AttackStage.enter;

            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animatorManager.keepAttack = true;
            animatorManager.AttackInvoke(AttackStage.enter);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float t = stateInfo.normalizedTime;
            if (stage == AttackStage.enter || stage == AttackStage.end)
            {
                animatorManager.AttackInvoke(AttackStage.start);
                stage = AttackStage.start;
                attackTimer++;
            }
            else if (stage == AttackStage.start)
            {
                if (t - attackTimer + 1 > attackNormalizedTime)
                {
                    animatorManager.AttackInvoke(AttackStage.keep);
                    stage = AttackStage.keep;
                }
            }
            else if (stage == AttackStage.keep)
            {
                if (t > attackTimer)
                {
                    animatorManager.AttackInvoke(AttackStage.end);
                    stage = AttackStage.end;
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stage == AttackStage.keep)
            {
                animatorManager.AttackInvoke(AttackStage.end);

            }

            stage = AttackStage.exit;
            animatorManager.AttackInvoke(AttackStage.exit);
        }

    }
}