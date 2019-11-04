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

        bool isPickUp;
        float DisToGround;
        RaycastHit[] raycastResult = new RaycastHit[1];

        Vector3 PositionTarget;
        Quaternion RotationTarget;
        const float _internalRotateSpeed = 8;


        // Start is called before the first frame update
        void Start()
        {

        }
        private void FixedUpdate()
        {



        }

        private void OnAnimatorMove()
        {
            if (playerState != PlayerState.pickUp)
            {
                Vector3 deltPos = _animator.deltaPosition;
                Vector3 pos = _rigidbody.position + new Vector3(deltPos.x, deltPos.y, deltPos.z);
                Vector3 nextPos = pos;
                nextPos.y += 0.3f;
                if(Physics.Raycast(nextPos,Vector3.down,out RaycastHit s, 0.4f,1))
                {
                    pos = s.point;
                }

                _rigidbody.MovePosition(pos);
                //_rigidbody.velocity = Vector3.zero;
                
                Vector3 moveDir = new Vector3(moveDelt.x, 0, moveDelt.y);
                if (!moveDelt.Equals(Vector3.zero))
                {
                    float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
                    angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
                    Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
                    _rigidbody.MoveRotation(qua);
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            Debug.Log(_rigidbody.position);
            if (!transform.localPosition.Equals(_rigidbody.position))
                transform.localPosition = _rigidbody.position;
            if (!transform.localRotation.Equals(_rigidbody.rotation))
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _rigidbody.rotation, _internalRotateSpeed * Time.deltaTime);




            //bool isDown = _rigidbody.velocity.y < -5f;
            //if (!(playerState == PlayerState.pickUp))
            //{

            //    if (isDown || DisToGround > 1f)
            //    {
            //        if (playerState == PlayerState.move)
            //        {
            //            _rigidbody.AddForce(transform.forward * 100);
            //        }
            //        playerState = PlayerState.down;
            //        _rigidbody.useGravity = true;
            //        _rigidbody.isKinematic = false;

            //    }
            //    else
            //    {
            //        if (moveDelt.Equals(Vector2.zero))
            //        {
            //            _rigidbody.useGravity = false;
            //            _rigidbody.isKinematic = true;
            //            playerState = PlayerState.normal;
            //        }
            //        else
            //        {
            //            _rigidbody.useGravity = true;
            //            _rigidbody.isKinematic = false;
            //            playerState = PlayerState.move;
            //        }
            //    }
            //}

            bool isDown = _rigidbody.velocity.y < -1f;
            Debug.Log(_rigidbody.velocity.y);
            if (isDown)
            {
                playerState = PlayerState.down;
            }
            else if (moveDelt.x != 0 || moveDelt.y != 0)
            {
                playerState = PlayerState.move;
            }
            else
            {
                playerState = PlayerState.normal;
            }


            if (isPickUp)
            {
                playerState = PlayerState.pickUp;
            }

            if (playerState == PlayerState.move)
            {
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
            }
            else if (playerState == PlayerState.normal)
            {
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
            }
            else if (playerState == PlayerState.pickUp)
            {
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
            }
            else if (playerState == PlayerState.down)
            {
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
            }





            _animator.SetFloat("x", moveDelt.x);
            _animator.SetFloat("y", moveDelt.y);
            _animator.SetBool("down", isDown);
        }


        void MoveAddDelt(Vector3 delt)
        {

        }

        IEnumerator WaitForAnimationEnd(System.Action Complete)
        {
            isPickUp = true;
            yield return null;
            var stateinfo = _animator.GetCurrentAnimatorStateInfo(0);
            float time = stateinfo.length;
            yield return new WaitForSeconds(time);
            Complete?.Invoke();
            isPickUp = false;
        }

        void PickUp()
        {

            StartCoroutine(WaitForAnimationEnd(() => playerState = PlayerState.normal));

            var renders = currentPickUp.GetComponentsInChildren<Renderer>();
            foreach (var item in renders)
            {
                item.enabled = false;
            }
            currentPickUp = null;

            playerState = PlayerState.pickUp;

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

        public void OnAim()
        {
            bool isAim = shootState == PlayerShootState.aim || shootState == PlayerShootState.aimshoot;
            if (isAim)
            {

                shootState = PlayerShootState.none;
            }
            else
            {
                shootState = PlayerShootState.aim;
            }

            _animator.SetFloat("shoot", isAim ? 1 : 0);
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