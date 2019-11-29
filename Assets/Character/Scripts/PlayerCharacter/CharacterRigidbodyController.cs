using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class CharacterRigidbodyController : MonoBehaviour
    {
        public STCharacterController characterController;


        public void RegisterPickUp(PickUp pickUp)
        {
            characterController.RegisterPickUp(pickUp);
        }
        public void RemovePickUp(PickUp pickUp)
        {
            characterController.RemovePickUp(pickUp);
        }



    }
}