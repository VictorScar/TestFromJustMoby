using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct TowerData
{
    [FormerlySerializedAs("CubeDatas")] public TowerCube[] CubesData;
    public Vector2 CubeSize;
}
