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

        public PlayerState playerState;
        public PlayerShootState shootState;
        [HideInInspector]
        public PickUp currentPickUp;

        Vector2 moveDelt;

        bool isMove;

        Vector3 PositionTarget;
        Quaternion RotationTarget;
        const float _internalRotateSpeed = 8;


        // Start is called before the first frame update
        void Start()
        {
            isMove = true;
        }
        private void FixedUpdate()
        {



        }

        private void OnAnimatorMove()
        {

            if (playerState == PlayerState.move)
            {
                Vector3 pos = _rigidbody.position + _animator.deltaPosition;
                _rigidbody.MovePosition(pos);

                Vector3 moveDir = new Vector3(moveDelt.x, 0, moveDelt.y);
                float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
                angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
                Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
                _rigidbody.MoveRotation(qua);
            }

            PositionTarget = _rigidbody.position;
            RotationTarget = _rigidbody.rotation;
        }
        // Update is called once per frame
        void Update()
        {

            if (!transform.localPosition.Equals(PositionTarget))
                transform.localPosition = PositionTarget;
            if (!transform.localRotation.Equals(RotationTarget))
                transform.localRotation = Quaternion.Slerp(transform.localRotation, RotationTarget, _internalRotateSpeed * Time.deltaTime);

            _animator.SetFloat("x", moveDelt.x);
            _animator.SetFloat("y", moveDelt.y);

            if (playerState == PlayerState.pickUp)
                return;
            if(_rigidbody.velocity.y<-0.1f||_rigidbody.velocity.y>0.2f)
            {
                if(playerState== PlayerState.move)
                {
                    _rigidbody.AddForce(transform.forward * 100);
                }
                playerState = PlayerState.down;
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
            }else
            {
                if(moveDelt.Equals(Vector2.zero))
                {
                    _rigidbody.useGravity = false;
                    _rigidbody.isKinematic = true;
                    playerState = PlayerState.normal;
                }
                else
                {
                    _rigidbody.useGravity = true;
                    _rigidbody.isKinematic = false;
                    playerState = PlayerState.move;
                }

            }
        }

        IEnumerator WaitForAnimationEnd(System.Action Complete)
        {
            isMove = false;
            yield return null;
            var stateinfo = _animator.GetCurrentAnimatorStateInfo(0);
            float time = stateinfo.length;
            yield return new WaitForSeconds(time);
            isMove = true;
            Complete?.Invoke();
        }

        public void PickUp()
        {
            _animator.Play("PickUp");

            StartCoroutine(WaitForAnimationEnd(() => playerState = PlayerState.normal));

            var renders = currentPickUp.GetComponentsInChildren<Renderer>();
            foreach (var item in renders)
            {
                item.enabled = false;
            }
            currentPickUp = null;

            playerState = PlayerState.pickUp;
        }

        public void Death()
        {
            _animator.Play("Death");

        }

        public void OnMove(InputValue value)
        {
            moveDelt = value.Get<Vector2>();
        }

        public void OnAim()
        {
            if (shootState == PlayerShootState.aim || shootState == PlayerShootState.aimshoot)
            {
                _animator.SetFloat("shoot", 0);
                shootState = PlayerShootState.none;
            }
            else
            {
                _animator.SetFloat("shoot", 1);
                shootState = PlayerShootState.aim;
            }
        }

        public void OnMainButtonPress()
        {
            Debug.Log("Start");


            if (currentPickUp)
            {
                PickUp();
            }
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