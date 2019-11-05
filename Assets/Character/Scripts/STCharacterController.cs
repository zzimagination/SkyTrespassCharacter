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
        RaycastHit[] raycastResult = new RaycastHit[4];

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
            if (playerState == PlayerState.move)
            {
                Vector3 deltPos = _animator.deltaPosition;
                MoveAddDelt(deltPos);
                RotateDelt(moveDelt);
            }
            if (playerState == PlayerState.down)
            {

                RotateDelt(moveDelt);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (!transform.localPosition.Equals(_rigidbody.position))
                transform.localPosition = _rigidbody.position;
            if (!transform.localRotation.Equals(_rigidbody.rotation))
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _rigidbody.rotation, _internalRotateSpeed * Time.deltaTime);
            bool isDown = _rigidbody.velocity.y < -2f;

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



        Vector3 nextGizmosPos;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(_rigidbody.position, 0.05f);
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(nextGizmosPos, 0.05f);
        }

        void MoveAddDelt(Vector3 delt)
        {
            if (delt.Equals(Vector3.zero))
                return;

            Vector3 pos = _rigidbody.position + new Vector3(delt.x, delt.y, delt.z);
            Vector3 next = pos;
            next.y += 0.3f;

            int c = Physics.RaycastNonAlloc(next, Vector3.down, raycastResult, 0.6f, -1);
            if (c > 0)
            {
                Vector3 tall;
                tall = raycastResult[0].point;
                for (int i = 1; i < c; i++)
                {
                    if (raycastResult[i].point.y > tall.y)
                    {
                        tall = raycastResult[i].point;
                    }
                }
                pos = tall;

                nextGizmosPos = pos;
            }

            _rigidbody.MovePosition(pos);
        }
        void RotateDelt(Vector3 delt)
        {
            if (delt.Equals(Vector3.zero))
                return;
            Vector3 moveDir = new Vector3(delt.x, 0, delt.y);
            float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
            angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
            Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
            _rigidbody.MoveRotation(qua);
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