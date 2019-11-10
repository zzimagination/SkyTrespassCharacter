using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
namespace SkyTrespass.Character
{
    public class STCharacterController : MonoBehaviour
    {

        public Rigidbody _rigidbody;
        public float moveSpeed;

        public bool  isAim;
        [HideInInspector]

        bool input;
        Vector2 moveDelt;
        Vector2 rotateDelt;
        PickUp currentPick;

        float DisToGround;
        RaycastHit[] raycastResult = new RaycastHit[4];

        Vector3 PositionTarget;
        Quaternion RotationTarget;
        const float _internalRotateSpeed = 8;
        const float _internalRunSpeed = 4.15f;
        const float _InternalWalkSpeed = 1.85f;

        Animator _animator;

        WeaponsType myWeapons; 

        public PickUp CurrentPick
        {
            get
            {
                return currentPick;
            }
            set
            {
                currentPick = value;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!transform.localPosition.Equals(_rigidbody.position))
                transform.localPosition = _rigidbody.position;
            if (!transform.localRotation.Equals(_rigidbody.rotation))
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _rigidbody.rotation, _internalRotateSpeed * Time.deltaTime);
            bool isDown = _rigidbody.velocity.y < -2f;

         

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _animator.SetFloat("state", 0);
                myWeapons = WeaponsType.none;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _animator.SetFloat("state", 1);
                myWeapons = WeaponsType.shoot;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _animator.SetFloat("state", 2);
                myWeapons = WeaponsType.pisol;
            }


            _animator.SetFloat("x", moveDelt.x);
            _animator.SetFloat("y", moveDelt.y);
            _animator.SetBool("down", isDown);
        }


#if UNITY_EDITOR
        Vector3 nextGizmosPos;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(_rigidbody.position, 0.05f);
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(nextGizmosPos, 0.05f);
        }

#endif
   
        IEnumerator WaitForAnimationEnd()
        {
            GetComponent<PlayerInput>().PassivateInput();
            yield return null;
            var stateinfo = _animator.GetCurrentAnimatorStateInfo(0);
            float time = stateinfo.length;
            yield return new WaitForSeconds(time);
            GetComponent<PlayerInput>().ActivateInput();
        }

        void PickUp()
        {
            CurrentPick.Pick();

            CurrentPick = null;
            _animator.SetTrigger("pick");
        }
        public bool attackLoop;
        public void Attack(bool attackLoop)
        {
            this.attackLoop = attackLoop;
            _animator.SetBool("attack", true);
            _animator.SetLayerWeight(1, 1);
        }

        public void EndAttack()
        {
            _animator.SetBool("attack", false);
            _animator.SetLayerWeight(1, 0);
        }


        public void StopRigidbody(bool s)
        {
            _rigidbody.useGravity = !s;
            _rigidbody.isKinematic = s;
        }

        public void MoveAddDelt()
        {
            if (moveDelt.Equals(Vector2.zero))
                return;

            Vector3 pos = _rigidbody.position + new Vector3(moveDelt.x, 0, moveDelt.y) * moveSpeed * Time.fixedDeltaTime;
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
        public void RotateDelt()
        {
            if (moveDelt.Equals(Vector2.zero))
                return;
            Vector3 moveDir = new Vector3(moveDelt.x, 0, moveDelt.y);
            float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
            angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
            Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
            _rigidbody.MoveRotation(qua);
        }

        public void SetPickUp(PickUp obj)
        {
            CurrentPick = obj;
        }


        public void Death()
        {
            _animator.Play("Death");
        }

        public void OnMove(InputValue value)
        {

            moveDelt = value.Get<Vector2>();
        }
        public void OnRotate(InputValue value)
        {
            rotateDelt = value.Get<Vector2>();
        }
        public void OnAim()
        {
            isAim = !isAim;
            moveSpeed = isAim ?_InternalWalkSpeed : _internalRunSpeed;

            _animator.SetFloat("aim", isAim ? 1 : 0);
        }

        public void OnMainButtonPress()
        {
            if (CurrentPick)
            {
                PickUp();
                StartCoroutine(WaitForAnimationEnd());
            }else
            {
                if(myWeapons!= WeaponsType.none)
                    Attack(false);
            }

        }
        public void OnMainButtonHold()
        {
            if(myWeapons!= WeaponsType.none)
                Attack(true);
        }
        public void OnMainButtonUp()
        {
            if (myWeapons != WeaponsType.none)
            {
                if (attackLoop)
                    EndAttack();
            }
        }
    }
}