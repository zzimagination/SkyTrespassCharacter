using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class CharacterRigidbodyController : MonoBehaviour
    {
        public STCharacterController characterController;




        public void EnterPickUp(PickUp pickUp)
        {
            characterController.currentPickUp = pickUp;
        }

        public void ExitPickUp()
        {
            characterController.currentPickUp = null;
        }
    }
}