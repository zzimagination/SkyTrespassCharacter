using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkyTrespass
{
    using Character;
    public class PickUp : MonoBehaviour
    {
        public bool open = true;

        bool isPicked = false;
        CharacterRigidbodyController characterRigidbodyController;
        private void OnTriggerEnter(Collider other)
        {
            if (open == false)
                return;
            if (other.CompareTag("Player"))
            {
                characterRigidbodyController = other.GetComponent<CharacterRigidbodyController>();
                characterRigidbodyController.RegisterPickUp(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (open == false||isPicked)
                return;
            characterRigidbodyController.RemovePickUp(this);
            
        }

        void TestPick(bool a)
        {
            var t = GetComponentsInChildren<Renderer>();
            foreach (var item in t)
            {
                item.enabled = a;
            }
            GetComponent<Collider>().enabled = a;
        }
        IEnumerator DelayOpen()
        {
            yield return new WaitForSeconds(2);
            isPicked = false;
            TestPick(true);
        }
        public void Pick()
        {
            TestPick(false);
            isPicked = true;
            StartCoroutine(DelayOpen());
        }




    }
}