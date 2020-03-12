using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkyTrespass.Goods
{
    using Character;
    public class PickUp : MonoBehaviour
    {
        public bool open = true;
        public int ID;


        public PickUpInfomation Pick()
        {
            PickUpInfomation infomation = new PickUpInfomation();
            infomation.id = ID;
            Destroy(gameObject);
            return infomation;
        }
    }


}