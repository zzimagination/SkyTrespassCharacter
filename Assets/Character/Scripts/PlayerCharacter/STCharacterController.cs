using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
namespace SkyTrespass.Character
{
    public class STCharacterController : MonoBehaviour
    {
        public Animator _animator;
        public Rigidbody _rigidbody;
        public float moveSpeed;

        public bool isAim;

        public Transform pistolRoot;
        public Transform rifleRoot;
        
        public bool keepAttack;

        Vector2 moveDelt;
        Vector2 rotateDelt;
        PickUp currentPick;

        float DisToGround;
        RaycastHit[] raycastResult = new RaycastHit[4];

        Vector3 PositionTarget;
        Quaternion RotationTarget;
        const float _internalRotateSpeed = 8;
        const float _internalRunSpeed = 4.2f;
        const float _InternalWalkSpeed = 2f;



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
        public WeaponsType MyWeapons
        {
            get
            {
                return myWeapons;
            }
            set
            {
                myWeapons = value;
                SetAimState(false);
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


            bool isMove = moveDelt.x != 0 || moveDelt.y != 0;
            _animator.SetBool("isMove", isMove);
            Vector3 move = transform.worldToLocalMatrix.MultiplyVector(new Vector3(moveDelt.x, 0, moveDelt.y));

            _animator.SetFloat("moveX", move.x);
            _animator.SetFloat("moveY", move.z);
            _animator.SetBool("down", isDown);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if(layerIndex==0)
            {

            }
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


        private void OnGUI()
        {
            GUILayout.Label("瞄准" + isAim.ToString());
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

        void SetAimState(bool aim)
        {
            isAim = aim;
            moveSpeed = isAim ? _InternalWalkSpeed : _internalRunSpeed;
            _animator.SetFloat("speed", isAim ? 0 : 1);
            _animator.SetBool("isAim", isAim);
        }


        public void ChangeWaepons(WeaponsType weapons)
        {
            _animator.SetInteger("weapons", (int)weapons);
            MyWeapons = weapons;
            GetComponent<EquipmentManager>().ChangeWeapons(weapons);
        }

        public void Attack()
        {

            _animator.SetBool("attack", true);
            //_animator.SetLayerWeight(1, 1);
            keepAttack = true;
        }

        public void EndAttack()
        {

            _animator.SetBool("attack", false);
            keepAttack = false;
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
            Vector2 v2 = rotateDelt;
            if (v2.x == 0 && v2.y == 0)
                v2 = moveDelt;
            if (v2.x == 0 && v2.y == 0)
                return;

            Vector3 moveDir = new Vector3(v2.x, 0, v2.y);
            float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
            angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
            Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
            _rigidbody.MoveRotation(qua);
        }

        public void SetPickUp(PickUp obj)
        {
            CurrentPick = obj;
        }


        public void HasGuns()
        {
            isAim = false;
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
            if (myWeapons == WeaponsType.shoot)
            {
                SetAimState(!isAim);
            }
            else
            {
                isAim = false;
                SetAimState(false);
            }

           
        }

        public void OnMainButtonPress()
        {
            if (CurrentPick)
            {
                PickUp();

            }
            else
            {
                Attack();
            }

        }
        public void OnMainButtonHold()
        {

        }
        public void OnMainButtonUp()
        {

            EndAttack();

        }


        public void OnButton1()
        {
            ChangeWaepons(WeaponsType.none);
        }
        public void OnButton2()
        {
            ChangeWaepons(WeaponsType.shoot);
        }
        public void OnButton3()
        {
            ChangeWaepons(WeaponsType.pisol);
        }

    }
}