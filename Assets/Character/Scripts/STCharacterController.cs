using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyTrespass.Character
{
    public class STCharacterController : MonoBehaviour
    {
        public Rigidbody _rigidbody;
        public Animator _animator;

        Vector2 moveDelt;
        bool isShoot;
        // Start is called before the first frame update
        void Start()
        {
            isShoot = false;
        }
        private void FixedUpdate()
        {

        }

        private void OnAnimatorMove()
        {
            if (!moveDelt.Equals(Vector3.zero))
            {
                Vector3 moveDir = new Vector3(moveDelt.x, 0, moveDelt.y);
                Vector3 pos = _rigidbody.position + _animator.deltaPosition;
                _rigidbody.MovePosition(pos);

                float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
                angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
                Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));

                _rigidbody.MoveRotation(qua);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (!transform.localPosition.Equals(_rigidbody.rotation))
                transform.localPosition = _rigidbody.position;
            if (!transform.localRotation.Equals(_rigidbody.rotation))
                transform.localRotation = _rigidbody.rotation;

            _animator.SetFloat("x",moveDelt.x);
            _animator.SetFloat("y", moveDelt.y);
            if (moveDelt.magnitude > 0)
            {
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
            
            }
            else
            {
                if (Mathf.Abs(_rigidbody.velocity.y) < 0.01f)
                {
                    _rigidbody.isKinematic = true;
                    _rigidbody.useGravity = false;
                }
            }


            if(Input.GetKeyDown(KeyCode.E))
            {
                _animator.Play("Death");
            }
        }



        public void PickUp()
        {
            _animator.Play("PickUp");
        }

        public void Death()
        {
            _animator.Play("Death");
        }

        public void OnMove(InputValue value)
        {
            moveDelt = value.Get<Vector2>();
        }

        public void OnShoot()
        {
            if(isShoot)
            {
                isShoot = false;
                _animator.SetFloat("shoot", 0);
            }else
            {
                isShoot = true;
                _animator.SetFloat("shoot", 1);
            }
        }

        public void OnMainButtonPress()
        {
            Debug.Log("Start");
        }
        public void OnMainButtonHold()
        {
            Debug.Log("Actioning");

           
        }
        public void OnMainButtonUp()
        {
            Debug.Log("End");
        }
    }
}