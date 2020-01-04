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
        public Transform rightHand;
        [HideInInspector]
        public bool isFall;

        bool isReloadBullet;
        bool isUnarm;
        Vector2 moveDelt;
        Vector2 rotateDelt;

        List<PickUp> pickUps = new List<PickUp>();

        public buttonAction MainButtonPress;
        public buttonAction MainButtonUp;

        public delegate void buttonAction();

        private void Awake()
        {
            animatorManager.EnterDeath += EnterDeath;
            animatorManager.Attack += attackMachine.Attack;
            animatorManager.EnterReload += EnterReloadBullet;
            animatorManager.ExitReload += ExitReloadBullet;
            attackMachine.StopAttackEvent += AttackMachineStop;
        }

        private void Start()
        {

            StartCoroutine(InitState());
        }

        void Update()
        {
            MoveCheck();

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
                ReloadBullets();
            }

        }
#endif


        IEnumerator InitState()
        {
            yield return null;
            equipment.InitEquipment();
            PickCurrentWeapons();
        }

        void MoveCheck()
        {
            Vector3 move = transform.worldToLocalMatrix.MultiplyVector(new Vector3(moveDelt.x, 0, moveDelt.y));
            bool isMove = moveDelt.x != 0 || moveDelt.y != 0;
            _animator.SetBool("isMove", isMove);
            _animator.SetFloat("moveX", move.x);
            _animator.SetFloat("moveY", move.z);
        }

        void AttackMachineStop()
        {
            StopAttack();
        }

        void SetUnarmAttack()
        {
            isUnarm = true;
            attackMachine.playDefault = true;
            attackMachine.StartAttack();
            if (attackMachine.DefaultAttack == null)
            {
                UnarmAttack unarmAttack = new UnarmAttack();
                unarmAttack.r_hand = rightHand;
                unarmAttack.unarmDamage = equipment.unarmDamage;
                unarmAttack.unarmAttackCheckRange = equipment.unarmAttackCheckRange;
                attackMachine.DefaultAttack = unarmAttack;
            }
            animatorManager.physics_MoveSpeed = equipment.GetMoveSpeed();
            _animator.SetInteger("weapons", 0);
        }

        void SetWeaponsAttack()
        {
            isUnarm = false;
            attackMachine.playDefault = false;
            attackMachine.StartAttack();
            attackMachine.CurrentAttack = equipment.currentWeapons;
            animatorManager.LeftHandIK = equipment.currentWeapons.leftHandIK;
            animatorManager.physics_MoveSpeed = equipment.GetMoveSpeed();
            _animator.SetInteger("weapons", (int)equipment.currentWeapons.weaponsType);
        }

        void ChangeAimState(bool _isAim)
        {
            if (isUnarm)
            {
                equipment.ChangeAim(false);
                _animator.SetBool("isAim", false);
            }
            else
            {
                equipment.ChangeAim(_isAim);
                _animator.SetBool("isAim", _isAim);
            }
            animatorManager.physics_MoveSpeed = equipment.GetMoveSpeed();
        }

        public void ReloadBullets()
        {
            if (equipment.CanReloadBullet())
            {
                PickCurrentWeapons();
                ChangeAimState(false);
                _animator.SetTrigger("bullet");
            }
        }

        public void EnterReloadBullet()
        {
            isReloadBullet = true;
            equipment.ReloadBullet();
        }
        public void ExitReloadBullet()
        {
            isReloadBullet = false;
        }

        public void AutoChangeAim()
        {
            if (isReloadBullet)
                return;
            if (animatorManager.isFall)
                return;
            if (isUnarm)
                return;
            ChangeAimState(!animatorManager.isAim);
        }


        public void MainButtonActionManager()
        {
            if (isReloadBullet)
                return;
            if (animatorManager.isFall)
                return;


            if (pickUps.Count > 0)
            {
                Pick();
                return;
            }

            Attack();
        }

        public void Pick()
        {
            pickUps[0].Pick();
            pickUps.RemoveAt(0);
            _animator.SetTrigger("pick");
        }

        public void Attack()
        {
            if (isUnarm)
            {
                _animator.SetBool("attack", true);
                return;
            }

            if (equipment.WeaponsCanAttack())
            {
                _animator.SetBool("attack", true);
            }
            else
            {
                PutCurrentWeapons();
            }
        }

        public void StopAttack()
        {
            _animator.SetBool("attack", false);
        }


        public void PickCurrentWeapons()
        {
            equipment.PickWeapons();
            if (equipment.currentWeapons)
                SetWeaponsAttack();
            else
                SetUnarmAttack();
            ChangeAimState(false);
        }
        public void PutCurrentWeapons()
        {
            ChangeAimState(false);
            equipment.PutWeapons();
            SetUnarmAttack();
        }

        public void ChangeWeapons()
        {
            if (animatorManager.isFall)
                return;
            if (isReloadBullet)
                return;
            if (animatorManager.keepAttack)
                StopAttack();
            ChangeAimState(false);
            PutCurrentWeapons();
            equipment.ChangeWeapons();
            PickCurrentWeapons();
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

        public void Relife()
        {

        }
        public void Death()
        {
            _animator.SetTrigger("death");

        }
        public void EnterDeath()
        {
            InputSwitch(false);
        }


        #region InputAction
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
            AutoChangeAim();
        }

        public void OnMainButtonPress()
        {
            MainButtonActionManager();
        }

        public void OnMainButtonUp()
        {
            StopAttack();
        }


        public void OnChangeWeapons()
        {
            ChangeWeapons();
        }

        public void OnReloadBullet()
        {
            ReloadBullets();
        }
        #endregion
    }
}