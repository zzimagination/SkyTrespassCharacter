using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Goods
{
    using SkyTrespass.Character;


    public struct PickUpInfomation
    {
        public int id;

    }

    public class ParseInformation
    {
        public PickType pickType;
        public int id;
        public string objPath;
    }

    public enum PickType
    {
        health,
        weapons
    }

    public static class ParseGoods
    {

        public static ParseInformation ParseID(int id)
        {
            ParseInformation information = new ParseInformation();
            information.id = id;
            int number = id / 10000;
           
            if (number == 1)
            {
                information.pickType = PickType.health;

            }
            else if (number == 2)
            {
                information.pickType = PickType.weapons;
                GetObjectPath(information);
            }

            return information;
        }

        public static void GetObjectPath(ParseInformation information)
        {
            if (information.pickType == PickType.weapons)
            {
                int number = information.id/10000;
                number = information.id - number*10000;
                if (number == 1)
                {
                    information.objPath = "Weapons/Rifle";
                }
            }
        }


    }



}