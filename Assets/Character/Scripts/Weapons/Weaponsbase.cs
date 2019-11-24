using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class Weaponsbase : MonoBehaviour
    {
        public WeaponsType weaponsType;
        public Transform leftIK;
        public Vector3 shootPoint;
        public GameObject bulletLinerObj;

        public WeaponsInfo weaponsInfo;

        public void Hidden()
        {
            gameObject.SetActive(false);
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public bool HasIK()
        {
            return leftIK != null;
        }
    }
}