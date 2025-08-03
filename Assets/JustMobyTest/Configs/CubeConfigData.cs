using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CubeConfigData
{
    public TowerCubeType CubeType;
    public Sprite Image;

    public CubeConfigData(TowerCubeType cubeType, Sprite image = null)
    {
        CubeType = cubeType;
        Image = image;
    }

    public static CubeConfigData Invalid => new CubeConfigData(TowerCubeType.None);
    public bool IsInvalid => CubeType == TowerCubeType.None;
}