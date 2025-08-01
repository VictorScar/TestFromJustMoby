using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerCube
{
    private TowerCubeType _cubeType;
    public TowerCubeType CubeType => _cubeType;

    public TowerCube(TowerCubeType cubeType)
    {
        _cubeType = cubeType;
    }
}