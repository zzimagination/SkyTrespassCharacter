using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
namespace SkyTrespass.Character
{
    public class STCharacterController : MonoBehaviour
    {
        public PlayerAnimatorManager animatorManager;
        public Animator _animator;
        public Rigidbody _rigidbody;
        public PlayerInput playerInput;
        public EquipmentManager equipment;
        public AttackMachine attackMachine;
        public float moveSpeed;

        [HideInInspector]
        public bool isGround;
        [HideInInspector]
        public bool isFall;
        [HideInInspector]
        public bool keepAttack;
        [HideInInspector]
        public bool prepareIdle;
        [HideInInspector]
        public bool isIK;

        bool hasAim;
        bool isAim = false;
        bool isChangeWeapons;

        //Vector2 moveDelt;
        //Vector2 rotateDelt;

        List<PickUp> pickUps;

        //float DisToGround;
        //RaycastHit[] raycastResult = new RaycastHit[16];

        const float _internalRotateSpeed = 8;
        const float _internalRunSpeed = 4.2f;
        const float _InternalWalkSpeed = 2f;

        public buttonAction AimButton;
        public buttonAction MainButtonPress;
        public buttonAction MainButtonUp;

        public delegate void buttonAction();

        private void Start()
        {
            MainButtonPress = PickOrAttack;
            AimButton = AutoChangeAim;
            MainButtonUp = StopAttack;

            pickUps = new List<PickUp>();
            StartCoroutine(InitState());
        }

        void Update()
        {
            //if (!transform.localPosition.Equals(_rigidbody.position))
            //    transform.localPosition = _rigidbody.position;
            //if (!transform.localRotation.Equals(_rigidbody.rotation))
            //    transform.localRotation = Quaternion.Slerp(transform.localRotation, _rigidbody.rotation, _internalRotateSpeed * Time.deltaTime);

            //bool isDown = _rigidbody.velocity.y < -2f;
            //bool isMove = moveDelt.x != 0 || moveDelt.y != 0;
            //Vector3 move = transform.worldToLocalMatrix.MultiplyVector(new Vector3(moveDelt.x, 0, moveDelt.y));

            //_animator.SetBool("isMove", isMove);
            //_animator.SetFloat("moveX", move.x);
            //_animator.SetFloat("moveY", move.z);
            //_animator.SetBool("down", isDown);
        }


#if UNITY_EDITOR

        private void OnGUI()
        {
            if (GUILayout.Button("Death"))
            {
                Death();
            }
            else if (GUILayout.Button("Relife"))
            {
                Relife();
            }else if(GUILayout.Button("Bullet"))
            {
                _animator.SetTrigger("bullet");
            }

        }
#endif


        IEnumerator InitState()
        {
            yield return null;
            equipment.InitWeapons();
            InitWeapons();
        }

        float GetAttackCD()
        {
            if (equipment.myWeaponsType == WeaponsType.none)
            {
                var u = equipment.characterInfo.unArmAttackInfo;
                return u.CD * u.CD_Per;
            }
            else
            {
                var s = equipment.characterInfo.weaponsAttackInfo.shootAttackInfo;
                return isAim ? s.shootAimCD * s.shootAimCD_Per : s.shootCD * s.shootCD_Per;
            }
        }

        public void AutoChangeAim()
        {
            if (hasAim)
            {
                ChangeAimState(!isAim);
            }
        }

        public void ChangeAimState(bool _isAim)
        {
            isAim = _isAim;
            moveSpeed = isAim ? _InternalWalkSpeed : _internalRunSpeed;
            animatorManager.physics_MoveSpeed = moveSpeed;
            attackMachine.isAim = isAim;
            _animator.SetBool("changeAim", true);
            _animator.SetFloat("attackSpeedMul", GetAttackCD());
            Debug.Log(GetAttackCD());
            _animator.SetFloat("speed", isAim ? 0 : 1);
            _animator.SetBool("isAim", isAim);
        }

        public void PickOrAttack()
        {
            if (pickUps.Count > 0)
            {
                ChangeAimState(false);
                pickUps[0].Pick();
                pickUps.RemoveAt(0);
                _animator.SetTrigger("pick");
            }
            else
            {
                if (!keepAttack)
                    _animator.SetBool("attack", true);
            }
        }

        public void InitWeapons()
        {
            ChangeAimState(false);
            WeaponsType type = equipment.myWeaponsType;
            hasAim = equipment.hasAim;
            if (type== WeaponsType.none)
            {
                _animator.SetInteger("weapons", 0);
                return;
            }
            _animator.SetInteger("weapons", (int)type);
        }





        public void ChangeWeapons()
        {
            isChangeWeapons = true;
            StopAttack();

            if (!keepAttack)
                ChangeWeaponsEnd();
        }
        public void ChangeWeaponsEnd()
        {
            if (!isChangeWeapons)
                return;
            equipment.ChangeWeapons();
            _animator.SetInteger("weapons", (int)equipment.myWeaponsType);

            ChangeAimState(false);
            isChangeWeapons = false;
        }


        public void EnterAttack()
        {
            keepAttack = true;
        }
        public void ExitAttack()
        {
            keepAttack = false;
        }
        public void StopAttack()
        {
            _animator.SetBool("attack", false);
        }

        public void InputSwitch(bool open)
        {
            if (open)
            {
                playerInput.ActivateInput();
            }
            else
            {
                playerInput.PassivateInput();

            }
        }

        public void StopRigidbody(bool s)
        {
            _rigidbody.useGravity = !s;
            _rigidbody.isKinematic = s;
        }

        public void Idle()
        {
            Vector3 checkPoint = _rigidbody.position;
            checkPoint.y += 0.1f;
            bool r = Physics.Raycast(checkPoint, Vector3.down, 0.2f);
            if (r)
            {
                StopRigidbody(true);
            }
            else
            {
                StopRigidbody(false);
            }

        }
        //public void MoveAddDelt()
        //{
        //    if (moveDelt.Equals(Vector2.zero))
        //        return;

        //    Vector3 pos = _rigidbody.position + new Vector3(moveDelt.x, 0, moveDelt.y) * moveSpeed * Time.fixedDeltaTime;
        //    Vector3 next = pos;
        //    next.y += 0.2f;
        //    if (Physics.Raycast(next, Vector3.down, out RaycastHit hitInfo, 0.4f))
        //    {
        //        pos = hitInfo.point;
        //    }
        //    _rigidbody.MovePosition(pos);
        //}
        //public void RotateDelt()
        //{
        //    Vector2 v2 = rotateDelt;
        //    if (v2.x == 0 && v2.y == 0)
        //        v2 = moveDelt;
        //    if (v2.x == 0 && v2.y == 0)
        //        return;

        //    Vector3 moveDir = new Vector3(v2.x, 0, v2.y);
        //    float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
        //    angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
        //    Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
        //    _rigidbody.MoveRotation(qua);
        //}

        public void RegisterPickUp(PickUp obj)
        {
            pickUps.Add(obj);
        }
        public void RemovePickUp(PickUp obj)
        {
            if (pickUps.Count == 0)
                return;
            if (pickUps.Contains(obj))
                pickUps.Remove(obj);
        }

        public void HasGuns()
        {
            ChangeAimState(false);
        }
        public void Relife()
        {
            //    _animator.Play("UnArmed");

            //    _animator.SetFloat("moveX", 0);
            //    _animator.SetFloat("moveY", 0);
            //    _animator.SetBool("down", false);
            //    _animator.SetBool("isDeath", false);
            //    _animator.SetBool("attack", false);
            //    _animator.SetBool("changeAim", false);
            //    _animator.SetBool("isAim", false);

            //    equipment.InitWeapons();
            //    InputSwitch(true);
        }
        public void Death()
        {
            _animator.SetTrigger("death");

        }



        public void OnMove(InputValue value)
        {

            var moveDelt = value.Get<Vector2>();
            animatorManager.moveDelt = moveDelt;
            bool isMove = moveDelt.x != 0 || moveDelt.y != 0;
            Vector3 move = transform.worldToLocalMatrix.MultiplyVector(new Vector3(moveDelt.x, 0, moveDelt.y));
            _animator.SetBool("isMove", isMove);
            _animator.SetFloat("moveX", move.x);
            _animator.SetFloat("moveY", move.z);
        }

        public void OnRotate(InputValue value)
        {
            var rotateDelt = value.Get<Vector2>();
            animatorManager.rotateDelt = rotateDelt;
        }

        public void OnAim()
        {
            AimButton?.Invoke();
        }

        public void OnMainButtonPress()
        {
            MainButtonPress?.Invoke();
        }

        public void OnMainButtonUp()
        {
            MainButtonUp?.Invoke();
        }


        public void OnChangeWeapons()
        {
            ChangeWeapons();
        }


    }
}