using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public enum PlayerState
    {
        normal,
        move,
        pickUp,
        down,
    }

    public enum PlayerShootState
    {
        none,
        shoot,
        aim,
        aimshoot,
    }
}
