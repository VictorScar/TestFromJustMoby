using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerCube
{
    public TowerCubeType CubeType;
    //public Vector2 Position;
    public float XPos;
    public int Height;

    public static TowerCube Invalid =>
        new()
        {
            CubeType = TowerCubeType.None
        };

    public bool IsInvalid => CubeType == TowerCubeType.None;

    public static bool operator ==(TowerCube a, TowerCube b)
    {
        return a.CubeType == b.CubeType;
    }
  
    public static bool operator !=(TowerCube a, TowerCube b)
    {
        return !(a == b);
    }

}