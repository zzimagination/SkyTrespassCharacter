using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character {
    public class PlayerFall :StateMachineBehaviour
    {
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller= animator.GetComponent<STCharacterController>();
            controller.RotateDelt();
            controller.StopRigidbody(false);
        }
    }
}