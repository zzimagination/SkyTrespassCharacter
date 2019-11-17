using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class Weaponsbase : MonoBehaviour
    {
        public WeaponsType weaponsType;

        public Transform leftIK;
        public Transform rightIK;

        public Transform shootPoint;

        public float attackCD;
        public float attackDistance;


        public GameObject bulletLinerObj;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public bool HasIK()
        {
            return leftIK != null;
        }
    }
}