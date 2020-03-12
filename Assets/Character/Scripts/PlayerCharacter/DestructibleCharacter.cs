using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyTrespass.Character
{

    public class DestructibleCharacter : MonoBehaviour,IDestructible
    {
        public float health;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Attack(AttackInfo attackInfo)
        {
            health = health - attackInfo.damage;
            if (health <= 0)
            {
                health = 0;
                Death();
                return;
            }
        }

        public virtual void Death()
        {
            Debug.Log("Death");
        }
    }

}