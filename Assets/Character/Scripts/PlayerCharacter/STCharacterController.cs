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

        [HideInInspector]
        public bool isFall;

        bool isAim = false;
        bool isChangeWeapons;

        Vector2 moveDelt;
        Vector2 rotateDelt;

        List<PickUp> pickUps;

        const float _internalRunSpeed = 4.2f;
        const float _InternalWalkSpeed = 2f;

        public buttonAction AimButton;
        public buttonAction MainButtonPress;
        public buttonAction MainButtonUp;

        public delegate void buttonAction();

        private void OnEnable()
        {
            animatorManager.EnterFall += EnterFall;
            animatorManager.ExitFall += ExitFall;
            animatorManager.EnterDeath += EnterDeath;

            animatorManager.Attack += attackMachine.Attack;
        }

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
            MoveCheck();
        }

        private void OnDisable()
        {
            animatorManager.EnterFall -= EnterFall;
            animatorManager.ExitFall -= ExitFall;
            animatorManager.EnterDeath -= EnterDeath;

            animatorManager.Attack += attackMachine.Attack;
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
            }
            else if (GUILayout.Button("Bullet"))
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

        void MoveCheck()
        {
            Vector3 move = transform.worldToLocalMatrix.MultiplyVector(new Vector3(moveDelt.x, 0, moveDelt.y));
            bool isMove = moveDelt.x != 0 || moveDelt.y != 0;
            _animator.SetBool("isMove", isMove);
            _animator.SetFloat("moveX", move.x);
            _animator.SetFloat("moveY", move.z);
        }

        void SetAttackCD()
        {
            if (equipment.myWeaponsType == WeaponsType.none)
            {
                var u = equipment.characterInfo.unArmAttackInfo;
                _animator.SetFloat("attackSpeedMul", u.CD * u.CD_Per);
            }
            else
            {
                var s = equipment.characterInfo.weaponsAttackInfo.shootAttackInfo;
                if(equipment.myWeaponsType== WeaponsType.pisol)
                {
                    _animator.SetFloat("attackSpeedMul", s.shootCD * s.shootCD_Per);
                }else if(equipment.myWeaponsType== WeaponsType.rifle)
                {
                    if (isAim)
                    {
                        _animator.SetFloat("attackSpeedMul", s.shootAimCD * s.shootAimCD_Per);
                    }
                    else
                    {
                        _animator.SetFloat("attackSpeedMul", s.shootCD * s.shootCD_Per);
                    }
                }
            }

        }
        void SetMoveSpeed()
        {
            animatorManager.moveSpeed = _internalRunSpeed;
            animatorManager.aimMoveSpeed = _InternalWalkSpeed;
        }

        public void AutoChangeAim()
        {
            ChangeAimState(!isAim);
        }
        public void ChangeAimState(bool _isAim)
        {
            isAim = _isAim;
            attackMachine.isAim = isAim;

            SetAttackCD();
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
                if (!animatorManager.keepAttack)
                {
                    _animator.SetBool("attack", true);
                }
            }
        }

        public void InitWeapons()
        {
            ChangeAimState(false);
            WeaponsType type = equipment.myWeaponsType;

            SetMoveSpeed();
            SetAttackCD();
            if(type== WeaponsType.rifle)
            {
                WeaponsRifle weapons = equipment.currentWeapons as WeaponsRifle;
                animatorManager.LeftHandIK = weapons.leftHandIK;
            }
            _animator.SetInteger("weapons", (int)type);
        }
        public void ChangeWeapons()
        {
            StopAttack();
            ChangeAimState(false);
            equipment.ChangeWeapons();
            WeaponsType type = equipment.myWeaponsType;

            SetMoveSpeed();
            SetAttackCD();
            if (type == WeaponsType.rifle)
            {
                WeaponsRifle weapons = equipment.currentWeapons as WeaponsRifle;
                animatorManager.LeftHandIK = weapons.leftHandIK;
            }
            _animator.SetInteger("weapons", (int)type);
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

        public void EnterFall()
        {
            MainButtonPress = null;
            MainButtonUp = null;
            AimButton = null;
            isFall = true;
        }
        public void ExitFall()
        {
            MainButtonPress = PickOrAttack;
            AimButton = AutoChangeAim;
            MainButtonUp = StopAttack;
            isFall = false;
        }
        public void EnterDeath()
        {
            InputSwitch(false);
        }



        public void OnMove(InputValue value)
        {
            moveDelt = value.Get<Vector2>();
            animatorManager.moveDelt = moveDelt;
        }

        public void OnRotate(InputValue value)
        {
            rotateDelt = value.Get<Vector2>();
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