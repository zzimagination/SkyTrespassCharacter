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
        public Vector3 shootPoint;
        [HideInInspector]
        public bool isFall;
        
        bool isReloadBullet;
        bool isUnarm;
        Vector2 moveDelt;
        Vector2 rotateDelt;

        List<PickUp> pickUps;

        public buttonAction MainButtonPress;
        public buttonAction MainButtonUp;

        public delegate void buttonAction();

        private void OnEnable()
        {
            animatorManager.EnterDeath += EnterDeath;
            animatorManager.Attack += attackMachine.Attack;
            animatorManager.EnterReload += EnterReloadBullet;
            animatorManager.ExitReload += ExitReloadBullet;
        }

        private void Start()
        {
            pickUps = new List<PickUp>();
            MainButtonPress = MainButtonActionManager;
            MainButtonUp = StopAttack;
            StartCoroutine(InitState());
        }

        void Update()
        {
            MoveCheck();

        }

        private void OnDisable()
        {
            animatorManager.EnterDeath -= EnterDeath;
            animatorManager.Attack -= attackMachine.Attack;
            animatorManager.EnterReload -= EnterReloadBullet;
            animatorManager.ExitReload -= ExitReloadBullet;
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
            attackMachine.playDefault = true;
            equipment.InitWeapons();
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

        void ShootOnceAttack()
        {

            var type = equipment.currentWeapons.weaponsType;
            int number = 0;
            switch (type)
            {
                case WeaponsType.none:
                    return;
                case WeaponsType.rifle:
                    var w1 = equipment.currentWeapons as WeaponsRifle;
                    number = w1.DoAttackNumber();
                    break;
                case WeaponsType.pistol:
                    var w2 = equipment.currentWeapons as WeaponsPistol;
                    number = w2.DoAttackNumber();
                    break;
                default:
                    break;
            }
            if (number == 0)
                StopAttack();
        }
        void SetUnarmAttack()
        {
            isUnarm = true;
            attackMachine.playDefault = true;
            if (attackMachine.DefaultCommand == null)
            {
                UnarmAttack unarmAttack = new UnarmAttack();
                unarmAttack.r_hand = rightHand;
                unarmAttack.unarmDamage = equipment.characterInfo.AttackInfo.unarmDamage;
                unarmAttack.unarmAttackCheckRange = equipment.characterInfo.AttackInfo.unarmAttackCheckRange;
            }
            _animator.SetInteger("weapons", 0);
            _animator.SetFloat("attackSpeedMul", equipment.GetUnarmAttackSpeedMul());
        }
        void SetWeaponsAttack()
        {
            isUnarm = false;
            attackMachine.playDefault = false;

            WeaponsType type = equipment.currentWeapons.weaponsType;
            switch (type)
            {
                case WeaponsType.none:
                    return;
                case WeaponsType.rifle:
                    WeaponsRifle weaponsrifle = equipment.currentWeapons as WeaponsRifle;
                    animatorManager.LeftHandIK = weaponsrifle.leftHandIK;
                    weaponsrifle.transform.SetParent(rightHand);
                    weaponsrifle.transform.localPosition = Vector3.zero;
                    weaponsrifle.transform.localRotation = Quaternion.identity;
                    weaponsrifle.shootPoint = shootPoint;
                    weaponsrifle.playerTransform = transform;
                    weaponsrifle.isAim = animatorManager.isAim;
                    weaponsrifle.TickEvent += ShootOnceAttack;
                    var ac= weaponsrifle.CreatAttackCommand();
                    attackMachine.SetAttackCommand(ac);
                    break;
                case WeaponsType.pistol:
                    WeaponsPistol weaponspistol = equipment.currentWeapons as WeaponsPistol;
                    weaponspistol.transform.SetParent(rightHand);
                    weaponspistol.transform.localPosition = Vector3.zero;
                    weaponspistol.transform.localRotation = Quaternion.identity;
                    weaponspistol.shootPoint = shootPoint;
                    weaponspistol.playerTransform = transform;
                    weaponspistol.TickEvent += ShootOnceAttack;
                    ac= weaponspistol.CreatAttackCommand();

                    attackMachine.SetAttackCommand(ac);
                    break;
                default:
                    return;
            }

            //SetAttackCommand(type);
            _animator.SetInteger("weapons", (int)type);
            _animator.SetFloat("attackSpeedMul", equipment.GetAttackSpeedMul());
        }
        void SetAttackCommand(WeaponsType type)
        {

            switch (type)
            {
                case WeaponsType.none:
                    UnarmAttackCommand unarmAttackCommand = new UnarmAttackCommand();
                    unarmAttackCommand.r_hand = rightHand;
                    unarmAttackCommand.unArmAttackInfo = equipment.characterInfo.AttackInfo;
                    attackMachine.SetDefaultCommand(unarmAttackCommand);
                    return;
                case WeaponsType.rifle:
                    var wr = equipment.currentWeapons as WeaponsRifle;
                    if (animatorManager.isAim)
                    {
                        var attackCommand = new RifleAttackCommand();
                        attackCommand.transform = transform;
                        attackCommand.localPoint = shootPoint;
                        attackCommand.info = equipment.characterInfo.AttackInfo;
                        attackCommand.TickEvent += ShootOnceAttack;
                        attackCommand.bulletLiner = wr.bulletLiner;
                        attackMachine.SetAttackCommand(attackCommand);
                    }
                    else
                    {
                        var attackCommand = new RifleAimAttackCommand();
                        attackCommand.transform = transform;
                        attackCommand.localPoint = shootPoint;
                        attackCommand.info = equipment.characterInfo.AttackInfo;
                        attackCommand.TickEvent += ShootOnceAttack;
                        attackMachine.SetAttackCommand(attackCommand);
                    }
                    break;
                case WeaponsType.pistol:
                    var pcmd = new PistolAttackCommand();
                    pcmd.transform = transform;
                    pcmd.localPoint = shootPoint;
                    pcmd.info = equipment.characterInfo.AttackInfo;
                    pcmd.TickEvent += ShootOnceAttack;
                    attackMachine.SetAttackCommand(pcmd);
                    break;
                default:
                    break;
            }
        }

        void ChangeAimState(bool _isAim)
        {
            animatorManager.isAim = _isAim;
            equipment.isAim = _isAim;
            animatorManager.physics_MoveSpeed = equipment.GetMoveSpeed();
            _animator.SetBool("isAim", _isAim);
            _animator.SetFloat("attackSpeedMul", equipment.GetAttackSpeedMul());
        }

        void ReloadBullets()
        {
            if (isReloadBullet)
                return;
            if (equipment.currentWeapons == null)
                return;
            if(equipment.currentWeapons.AttackNumber==equipment.currentWeapons.MaxAttackNumber())
            {
                return;
            }

            PickCurrentWeapons();
            _animator.SetTrigger("bullet");
        }

        public void EnterReloadBullet()
        {
            isReloadBullet = true;
        }
        public void ExitReloadBullet()
        {
            equipment.ReloadBullet();
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
            SetAttackCommand(equipment.currentWeapons.weaponsType);
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

            if (equipment.currentWeapons.AttackNumber == 0)
            {
                PutCurrentWeapons();
                return;
            }
            _animator.SetBool("attack", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("attack", false);
        }


        public void PickCurrentWeapons()
        {
            ChangeAimState(false);
            equipment.PickWeapons();
            if (equipment.currentWeapons)
                SetWeaponsAttack();
            else
                SetUnarmAttack();
        }
        public void PutCurrentWeapons()
        {
            ChangeAimState(false);
            StopAttack();
            equipment.PutWeapons();
            SetUnarmAttack();
        }

        public void ChangeWeapons()
        {
            if (animatorManager.isFall)
                return;
            if (animatorManager.keepAttack)
                StopAttack();
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

        #endregion
    }
}