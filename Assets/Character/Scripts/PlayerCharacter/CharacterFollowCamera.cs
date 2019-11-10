using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkyTrespass
{
    public class CharacterFollowCamera : MonoBehaviour
    {
        public Transform target;

        Vector3 dis;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            dis = transform.localPosition - target.localPosition;
        }
        // Update is called once per frame
        void Update()
        {

        }

        private void LateUpdate()
        {
            transform.localPosition = target.localPosition + dis;
        }
    }
}