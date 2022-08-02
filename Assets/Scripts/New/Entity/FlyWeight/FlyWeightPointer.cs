using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlyWeightPointer
{
    public static readonly FlyWeight Golem = new FlyWeight
    {
        speed = 2,
    };

    public static readonly FlyWeight Character = new FlyWeight
    {
        speed = 4,
    };
    public static readonly FlyWeight Lizard = new FlyWeight
    {
        speed = 2.5f,
    };
}
