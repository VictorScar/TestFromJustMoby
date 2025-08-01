using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerCubeData
{
    public TowerCubeType CubeType;
    public Sprite Image;

    public TowerCubeData(TowerCubeType cubeType, Sprite image = null)
    {
        CubeType = cubeType;
        Image = image;
    }

    public static TowerCubeData Invalid => new TowerCubeData(TowerCubeType.None);
    public bool IsInvalid => CubeType == TowerCubeType.None;
}