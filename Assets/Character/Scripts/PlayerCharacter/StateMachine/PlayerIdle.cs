using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace SkyTrespass.Character
{
    public class PlayerIdle : StateMachineBehaviour
    {
        STCharacterController characterController;
        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            if (!characterController)
            {
                characterController = animator.GetComponent<STCharacterController>();
            }

        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
           // animatorManager.TransformUpdate();
           // animatorManager.canPick = true;
           // animatorManager.canAttack = true;
           // animatorManager.canChangeWeapons = true;
        }
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            characterController.Rotate();
            //animatorManager.RotateDelt();
            //animatorManager.CheckDown();
            //animatorManager.Idle();

        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animatorManager.canPick = false;
            //animatorManager.canAttack = false;
           // animatorManager.canChangeWeapons = false;
        }
    }
}