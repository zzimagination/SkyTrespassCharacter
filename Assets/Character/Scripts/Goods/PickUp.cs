using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkyTrespass {
    using Character;
    public class PickUp : MonoBehaviour
    {
        bool open;
        private void Start()
        {
            open = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (open == false)
                return;
            if(other.CompareTag("Player"))
            {
                other.GetComponent<CharacterRigidbodyController>().SetPickUp(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var t= GetComponentsInChildren<Renderer>();
            foreach (var item in t)
            {
                item.enabled = true;
            }
            open = true;
        }

        public void Pick()
        {
            var rs= GetComponentsInChildren<Renderer>();
            foreach (var item in rs)
            {
                item.enabled = false;
            }
            open = false;
        }
    }
}