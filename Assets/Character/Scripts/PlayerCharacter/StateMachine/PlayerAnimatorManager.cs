using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        public Animator _animator;
        public Rigidbody _rigidbody;

        [HideInInspector]
        public float physics_MoveSpeed;
        [ReadOnly]
        public int weaponsInterger;
        [ReadOnly]
        public Vector2 moveDelt;
        [ReadOnly]
        public Vector2 rotateDelt;
        [ReadOnly]
        public float moveSpeed;
        [ReadOnly]
        public float aimMoveSpeed;
        [ReadOnly]
        public float attackSpeed;
        [ReadOnly]
        public float aimAttackSpeed;
        [HideInInspector]
        public bool keepAttack;
        [HideInInspector]
        public Transform LeftHandIK;


        const float _internalRotateSpeed = 8;
        const float _internalRunSpeed = 4.2f;
        const float _InternalWalkSpeed = 2f;

        public event AnimationEvent EnterFall;
        public event AnimationEvent ExitFall;
        public event AnimationEvent EnterDeath;
        public event AnimationEvent<AttackStage> Attack;

        public delegate void AnimationEvent();
        public delegate void AnimationEvent<T>(T a1);

        

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            _animator.SetFloat("moveX", 0);
            _animator.SetFloat("moveY", 0);
            _animator.SetFloat("attackSpeedMul", 1);
            _animator.SetInteger("weapons",weaponsInterger);
            _animator.SetBool("down", false);
            _animator.SetBool("isDeath", false);
            _animator.SetBool("attack", false);
            _animator.SetBool("isMove", false);
            _animator.SetBool("isAim", false);
            _animator.ResetTrigger("pick");
            _animator.ResetTrigger("death");
            _animator.ResetTrigger("bullet");
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            bool isDown = _rigidbody.velocity.y < -2f;
            _animator.SetBool("down", isDown);
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
        public void StopRigidbody(bool s)
        {
            _rigidbody.useGravity = !s;
            _rigidbody.isKinematic = s;
        }

        public void TransformUpdate()
        {
            if (!transform.localPosition.Equals(_rigidbody.position))
                transform.localPosition = _rigidbody.position;
            if (!transform.localRotation.Equals(_rigidbody.rotation))
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _rigidbody.rotation, _internalRotateSpeed * Time.deltaTime);
        }
        public void MoveAddDelt()
        {
            float x = moveDelt.x;
            float y = moveDelt.y;
            if (x == 0 && y == 0)
                return;
            Vector3 pos = _rigidbody.position + new Vector3(x, 0, y) * physics_MoveSpeed * Time.fixedDeltaTime;
            Vector3 next = pos;
            next.y += 0.2f;
            if (Physics.Raycast(next, Vector3.down, out RaycastHit hitInfo, 0.4f))
            {
                pos = hitInfo.point;
            }
            _rigidbody.MovePosition(pos);
        }
        public void RotateDelt()
        {
            float x = rotateDelt.x;
            float y = rotateDelt.y;
            if (x == 0 && y == 0)
            {
                x = moveDelt.x;
                y = moveDelt.y;
            }
            if (x == 0 && y == 0)
                return;

            Vector3 moveDir = new Vector3(x, 0, y);
            float angle = Vector3.Angle(new Vector3(0, 0, 1), moveDir);
            angle *= Vector3.Dot(new Vector3(1, 0, 0), moveDir) > 0 ? 1 : -1;
            Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
            _rigidbody.MoveRotation(qua);
        }

        public void Aim(bool isAim)
        {
            _animator.SetBool("isAim",isAim);
            _animator.SetBool("changeAim", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("attack", false);
        }

        public void EnterFallInvoke()
        {
            EnterFall?.Invoke();
        }
        public void ExitFallInvoke()
        {
            ExitFall?.Invoke();
        }
        public void EnterDeathInvoke()
        {
            EnterDeath?.Invoke();
        }

        public void AttackInvoke(AttackStage a1)
        {
            Attack?.Invoke(a1);
        }
    }

    public enum AttackStage
    {
        enter,
        start,
        keep,
        end,
        exit,
    }
}