using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrespass.Character
{
    public abstract class Weaponsbase : MonoBehaviour
    {
        public WeaponsType weaponsType;
        public AttackCommand attackCommand;
        public virtual void Hidden()
        {
            gameObject.SetActive(false);
        }
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        public virtual void Drop()
        {
            Destroy(gameObject);
        }


        public abstract void AddCharacterInfo(WeaponsAttackInfo characterInfo);
        public abstract void SubCharacterInfo(WeaponsAttackInfo characterInfo);

    }
}