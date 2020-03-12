using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        public Animator _animator;
        
        public int oldWeaponsType;
        public int newWeaponsType;

        public Transform LeftHandIK;

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            _animator.SetFloat("moveX", 0);
            _animator.SetFloat("moveY", 0);
            _animator.SetInteger("weaponsType", 0);
            _animator.SetBool("down", false);
            _animator.SetBool("isDeath", false);
            _animator.SetBool("attack", false);
            _animator.SetBool("isMove", false);
            _animator.SetBool("isAim", false);
            _animator.SetBool("isDeath", false);
            _animator.ResetTrigger("pick");
            _animator.ResetTrigger("bullet");
        }
        #region 动画事件

        #endregion
    }

    public enum AttackStage
    {
        enter,
        start,
        update,
        tick,
        end,
        exit,
    }
}