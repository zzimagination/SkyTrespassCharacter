using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : StateMachineBehaviour
{
    // Start is called before the first frame update
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("3");
    }
}
