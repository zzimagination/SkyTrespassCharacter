using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class TrainingRobot : DestructibleCharacter
    {
        public Animator myAnimator;
        public Collider myCollider;


        public override void Death()
        {
            myCollider.enabled = false;
            myAnimator.SetBool("death", true);
            
            Destroy(gameObject, 3);
        }

    }
}