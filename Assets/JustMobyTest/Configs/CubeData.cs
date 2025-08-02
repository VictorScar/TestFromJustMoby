using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CubeData
{
    public TowerCubeType CubeType;
    public Sprite Image;

    public CubeData(TowerCubeType cubeType, Sprite image = null)
    {
        CubeType = cubeType;
        Image = image;
    }

    public static CubeData Invalid => new CubeData(TowerCubeType.None);
    public bool IsInvalid => CubeType == TowerCubeType.None;
}