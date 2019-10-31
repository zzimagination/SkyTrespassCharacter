using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkyTrespass {
    using Character;
    public class PickUp : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var c= other.GetComponent<CharacterRigidbodyController>();
                c.EnterPickUp(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var c = other.GetComponent<CharacterRigidbodyController>();
                c.ExitPickUp();

                var r = GetComponentsInChildren<Renderer>();
                foreach (var item in r)
                {
                    item.enabled = true;
                }
            }
        }
    }
}