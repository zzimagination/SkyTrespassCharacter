using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
namespace SkyTrespass
{
    using Character;
    using Goods;
    public class STCharacterController : MonoBehaviour
    {
        public PlayerAnimatorManager animatorManager;
        public MoveController moveController;
        public Animator _animator;
        public PlayerInput playerInput;
        public EquipmentManager equipment;
        public Transform rightHand;



        int weaponsType;
        int weaponIndex;
        bool isAim;
        bool isFall;
        bool isUnarm;
        Vector2 moveDelt;
        Vector2 rotateDelt;

        public System.Action MainButtonDown;
        public System.Action MainButtonUp;

        private void Start()
        {
            MainButtonUp = StopAttack;
            StartCoroutine(InitState());
        }

        void Update()
        {
            CheckDown();
            MoveInput();


            if (equipment.tempBackpack.PickNumber == 0)
            {
                MainButtonDown = Attack;
            }
            else
            {
                MainButtonDown = Pick;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickObject"))
            {
                var t = other.GetComponent<PickUp>();
                equipment.tempBackpack.RegisterPickUp(t);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PickObject"))
            {
                var t = other.GetComponent<PickUp>();
                equipment.tempBackpack.RemovePickUp(t);
            }
        }
#if UNITY_EDITOR
        int t;
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
            else if (GUILayout.Button("PickUp"))
            {
                Pick();
            }
            else if (GUILayout.Button("A"))
            {
                SetAim(!isAim);
            }
            else if (GUILayout.Button("Change1"))
            {
                int last = weaponsType;
                weaponsType++;
                if (weaponsType > 2)
                    weaponsType = 0;
                AnimatorChangeWeapons(last, weaponsType);
            }

            GUILayout.Label("可拾取物品数量" + equipment.tempBackpack.PickNumber);
            GUILayout.Label("生命值" + equipment.Health);
            GUILayout.Label("弹药" + equipment.bulletsNumber);
        }
#endif


        IEnumerator InitState()
        {
            equipment.InitEquipment();

            yield return null;

            equipment.UpWeapons(0);
            equipment.PickWeapons(0);
            _animator.SetInteger("weaponsType", (int)equipment.currentWeapons.weaponsType);
            animatorManager.LeftHandIK = equipment.currentWeapons.leftHandIK;
            SetMoveSpeed(equipment.GetMoveSpeed());
        }

        void MoveInput()
        {
            Vector3 move = transform.worldToLocalMatrix.MultiplyVector(new Vector3(moveDelt.x, 0, moveDelt.y));
            move = move.normalized;
            _animator.SetBool("isMove", moveDelt.x != 0 || moveDelt.y != 0);
            _animator.SetFloat("moveX", move.x);
            _animator.SetFloat("moveY", move.z);
        }

        void CheckDown()
        {
            isFall = moveController.isFall;
            _animator.SetBool("down", isFall);
        }

        void SetMoveSpeed(float speed)
        {
            moveController.physics_MoveSpeed = speed;
        }

        void AnimatorChangeWeapons(int oldType, int newType)
        {
            if (oldType == newType)
                return;
            _animator.SetLayerWeight(1, 1);
            _animator.SetTrigger("changeWeapons");
            _animator.SetBool("isAim", false);
            animatorManager.oldWeaponsType = oldType;
            animatorManager.newWeaponsType = newType;
        }


        public void Move()
        {
            moveController.MoveDelt(moveDelt.x, moveDelt.y);

        }
        public void Rotate()
        {
            if (rotateDelt == Vector2.zero)
                moveController.RotateDelt(moveDelt.x, moveDelt.y);
            else
                moveController.RotateDelt(rotateDelt.x, rotateDelt.y);
        }

        public void SetAim(bool aim)
        {
            isAim = aim;
            _animator.SetBool("isAim", isAim);
            equipment.isAim = isAim;
        }


        public void DoingChangeWeaons()
        {
            equipment.PutWeapons(equipment.WeaponsIndex);

            equipment.PickWeapons(++equipment.WeaponsIndex);
            equipment.UpWeapons(equipment.WeaponsIndex);
            animatorManager.LeftHandIK = equipment.currentWeapons.leftHandIK;
        }

        public void ReloadBullets()
        {
            _animator.SetBool("attack", false);
            _animator.SetTrigger("bullet");
        }
        public void ReloadBulletComplete()
        {
            equipment.ReloadBulletComplete();
        }

        public void Pick()
        {

            //equipment.PickObject();
            _animator.SetTrigger("pick");

        }

        public void PickEnd()
        {

            equipment.PickObject();
            //if (equipment.lastPickup.pickType == Goods.PickType.weapons)
            //{
            //    // SetWeapons();
            //}
            Debug.Log("111");
        }


        public void Attack()
        {
            if (equipment.currentWeapons.RemainBullet > 0)
            {
                _animator.SetBool("attack", true);
            }
        }

        public void AttackProcess(AttackStage stage)
        {
            var w = equipment.currentWeapons;
            switch (stage)
            {
                case AttackStage.enter:
                    w.AttackPrepare();
                    break;
                case AttackStage.start:
                    w.AttackStart();
                    break;
                case AttackStage.update:
                    w.AttackUpdate();
                    break;
                case AttackStage.tick:
                    w.AttackTick();
                    if (w.RemainBullet <= 0)
                    {
                        ReloadBullets();
                    }
                    break;
                case AttackStage.end:
                    w.AttackEnd();
                    break;
                case AttackStage.exit:
                    w.AttackExit();
                    break;
            }
        }

        public void StopAttack()
        {
            _animator.SetBool("attack", false);
            MainButtonDown = Attack;
        }

        public void Relife()
        {
            _animator.SetBool("isDeath", false);
        }
        public void Death()
        {
            _animator.SetTrigger("dead");
            _animator.SetBool("isDeath", true);
        }

        #region InputAction
        public void OnMove(InputValue value)
        {
            moveDelt = value.Get<Vector2>();
            //animatorManager.moveDelt = moveDelt;
        }

        public void OnRotate(InputValue value)
        {
            rotateDelt = value.Get<Vector2>();
           // animatorManager.rotateDelt = rotateDelt;
        }

        public void OnAim()
        {
            if (isFall)
                return;
            SetAim(!isAim);
            SetMoveSpeed(equipment.GetMoveSpeed());
        }

        public void OnMainButtonPress()
        {
            MainButtonDown?.Invoke();
        }

        public void OnMainButtonUp()
        {
            MainButtonUp?.Invoke();
        }


        public void OnChangeWeapons()
        {
            if (isFall)
                return;
            SetAim(false);

            int old = (int)equipment.currentWeapons.weaponsType;
            equipment.WeaponsIndex = 1 + equipment.WeaponsIndex;
            int newT = (int)equipment.weaponsArray[equipment.WeaponsIndex].weaponsType;
            --equipment.WeaponsIndex;

            AnimatorChangeWeapons(old, newT);
            SetMoveSpeed(equipment.GetMoveSpeed());
        }

        public void OnReloadBullet()
        {
            StopAttack();
            ReloadBullets();
        }
        #endregion
    }
}