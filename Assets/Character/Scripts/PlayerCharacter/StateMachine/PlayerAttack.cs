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
        protected STCharacterController characterController;


        protected virtual void Attack(AttackStage stage)
        {
            characterController.AttackProcess(stage);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            attackTimer = 0;
            animator.SetLayerWeight(1, 1);
            stage = AttackStage.enter;

            if(!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();

            if (!characterController)
                characterController= animator.GetComponent<STCharacterController>();
            Attack(AttackStage.enter);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float t = stateInfo.normalizedTime;
            if (stage == AttackStage.enter || stage == AttackStage.end)
            {
                Attack(AttackStage.start);
                stage = AttackStage.start;
                attackTimer++;
            }
            else if (stage == AttackStage.start)
            {
                if (t - attackTimer + 1 > attackNormalizedTime)
                {
                    Attack(AttackStage.tick);
                    stage = AttackStage.update;
                }else
                {
                    Attack(AttackStage.update);
                }
            }
            else if (stage == AttackStage.update)
            {
                if (t > attackTimer)
                {
                    Attack(AttackStage.end);
                    stage = AttackStage.end;
                }else
                {
                    Attack(AttackStage.update);
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stage != AttackStage.end)
            {

                Attack(AttackStage.end);
            }

            stage = AttackStage.exit;
            Attack(AttackStage.exit);
        }



    }
}