using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerCube
{
    public TowerCubeType CubeType;
    public Vector2 Position;

    public static TowerCube Invalid =>
        new()
        {
            CubeType = TowerCubeType.None
        };

    public bool IsInvalid => CubeType == TowerCubeType.None;

}