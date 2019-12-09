using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerBullet : StateMachineBehaviour
    {
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {

            animator.SetLayerWeight(1, 1);
        }

    }
}