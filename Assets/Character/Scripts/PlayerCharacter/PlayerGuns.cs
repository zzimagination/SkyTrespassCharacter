using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{

    public class PlayerGuns : StateMachineBehaviour
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller= animator.GetComponent<STCharacterController>();
            controller.HasGuns();
        }

    }
}